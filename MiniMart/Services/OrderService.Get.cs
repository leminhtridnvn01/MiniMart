using Microsoft.EntityFrameworkCore;
using MiniMart.API.Extensions;
using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.DTOs.Locations;
using MiniMart.Domain.DTOs.Orders;
using MiniMart.Domain.DTOs.Products;
using MiniMart.Domain.DTOs.Stores;

namespace MiniMart.API.Services
{
    public partial class OrderService
    {
        public async Task<List<CityResponse>> GetCitiesForOrder()
        {
            return await _cityRepository.GetQuery(x => !x.IsDelete)
                                        .Select(x => new CityResponse
                                                {
                                                    CityId = x.Id,
                                                    CityName = x.Name ?? ""
                                                })
                                        .ToListAsync();
        }

        public async Task<List<GetStoreLocationResponse>> GetStores(int cityId)
        {
            return await _storeRepository.GetQuery(x => x.Ward.District.City.Id == cityId)
                                         .Select(new GetStoreLocationResponse().GetSelection())
                                         .ToListAsync();
        }

        public async Task<PagingResult<GetOrderResponse>> GetOrders(GetOrderRequest request)
        {
            return await _orderRepository.GetQuery(x => x.User.Id == _user.GetUserId()
                                                        && request.OrderStatus.HasValue 
                                                           ? x.LK_OrderStatus.Value == request.OrderStatus.Value
                                                           : true)
                                         .OrderByDescending(x => x.CreateOn)
                                         .Select(x => new GetOrderResponse()
                                         {
                                             OrderId = x.Id,
                                             StoreName = x.Store.Name ?? "Unknown",
                                             StoreId = x.Store.Id,
                                             OrderStatus = x.LK_OrderStatus.HasValue ? x.LK_OrderStatus.Value : Domain.Enums.LK_OrderStatus.None,
                                             TotalPrice = x.ProductDetails.Sum(pd => pd.TotalPrice.GetValueOrDefault()),
                                             UserName = x.User.Name ?? "Unknown",
                                             DeliveryAddress = x.DeliveryAddress ?? "",
                                             ContactPhoneNumber = x.User.PhoneNumber ?? "",
                                             OrderType = (int)x.LK_OrderType.Value,
                                             PaymentMethod = (int)x.LK_PaymentMethod.Value,
                                             PickupTime = x.PickupTimeFrom,
                                             IsApproved = x.IsApproved.HasValue ? x.IsApproved.Value : false,
                                             Products = x.ProductDetails.Select(pd => new GetProductInCartResponse()
                                             {
                                                 Id = pd.Product.Id,
                                                 Name = pd.Product.Name,
                                                 Img = pd.Product.Img,
                                                 Description = pd.Product.Description,
                                                 Price = pd.Product.Price,
                                                 PriceDecreases = (pd.Product.ProductStores.FirstOrDefault(ps => ps.Store.Id == x.Store.Id).PriceDecreases.HasValue 
                                                                   && pd.Product.ProductStores.FirstOrDefault(ps => ps.Store.Id == x.Store.Id).PriceDecreases.Value > 0)
                                                                   ? pd.Product.ProductStores.FirstOrDefault(ps => ps.Store.Id == x.Store.Id).PriceDecreases.Value
                                                                   : pd.Product.PriceDecreases,
                                                 LK_ProductUnit = pd.Product.LK_ProductUnit,
                                                 CategoryId = pd.Product.Category != null ? pd.Product.Category.Id : 0,
                                                 Quantity = pd.Quantity.HasValue ? pd.Quantity.Value : 0,
                                             })
                                         })
                                         .ToPagedListAsync(request.PageNo, request.PageSize);
        }

        public async Task<List<GetOrderParrentResponse>> GetOrdersVer2(GetOrderRequest request)
        {
            var a = await _orderParrentRepository.GetQuery(x => x.User.Id == _user.GetUserId()
                                                                            && request.OrderStatus.HasValue
                                                                               ? x.LK_OrderStatus.Value == request.OrderStatus.Value
                                                                               : true).ToListAsync();
            var c = await _orderParrentRepository.GetAllAsync();
            var orderParrents = await _orderParrentRepository.GetQuery(x => x.User.Id == _user.GetUserId()
                                                                            && request.OrderStatus.HasValue
                                                                               ? x.LK_OrderStatus.Value == request.OrderStatus.Value
                                                                               : true)
                                                             .OrderByDescending(x => x.CreateOn)
                                                             .Select(pr => new GetOrderParrentResponse()
                                                                     {
                                                                         TotalPrice = pr.TotalPrice,
                                                                         OrderParrentId = pr.Id,
                                                                         OrderStatus = pr.LK_OrderStatus.HasValue ? pr.LK_OrderStatus.Value : Domain.Enums.LK_OrderStatus.None,
                                                                         PaymentMethod = (int)pr.LK_PaymentMethod.Value,
                                                                         Orders = pr.Orders.Select(x => new GetOrderResponse()
                                                                         {
                                                                             OrderId = x.Id,
                                                                             StoreName = x.Store.Name ?? "Unknown",
                                                                             StoreId = x.Store.Id,
                                                                             OrderStatus = x.LK_OrderStatus.HasValue ? x.LK_OrderStatus.Value : Domain.Enums.LK_OrderStatus.None,
                                                                             TotalPrice = x.ProductDetails.Sum(pd => pd.TotalPrice.GetValueOrDefault()),
                                                                             UserName = x.User.Name ?? "Unknown",
                                                                             DeliveryAddress = x.DeliveryAddress ?? "",
                                                                             ContactPhoneNumber = x.User.PhoneNumber ?? "",
                                                                             OrderType = (int)x.LK_OrderType.Value,
                                                                             PaymentMethod = (int)x.LK_PaymentMethod.Value,
                                                                             PickupTime = x.PickupTimeFrom,
                                                                             Products = x.ProductDetails.Select(pd => new GetProductInCartResponse()
                                                                             {
                                                                                 Id = pd.Product.Id,
                                                                                 Name = pd.Product.Name,
                                                                                 Img = pd.Product.Img,
                                                                                 Description = pd.Product.Description,
                                                                                 Price = pd.Product.Price,
                                                                                 PriceDecreases = (pd.Product.ProductStores.FirstOrDefault(ps => ps.Store.Id == x.Store.Id).PriceDecreases.HasValue
                                                                                                   && pd.Product.ProductStores.FirstOrDefault(ps => ps.Store.Id == x.Store.Id).PriceDecreases.Value > 0)
                                                                                                   ? pd.Product.ProductStores.FirstOrDefault(ps => ps.Store.Id == x.Store.Id).PriceDecreases.Value
                                                                                                   : pd.Product.PriceDecreases,
                                                                                 LK_ProductUnit = pd.Product.LK_ProductUnit,
                                                                                 CategoryId = pd.Product.Category != null ? pd.Product.Category.Id : 0,
                                                                                 Quantity = pd.Quantity.HasValue ? pd.Quantity.Value : 0,
                                                                             })
                                                                         })
                                                                     })
                                                             .ToListAsync();
            return orderParrents;
        }

        public async Task<PagingResult<GetOrderResponse>> GetMangerOrders(GetManagerOrderRequest request)
        {
            return await _orderRepository.GetQuery(x => (request.OrderStatus.HasValue 
                                                        ? x.LK_OrderStatus.Value == request.OrderStatus.Value
                                                        : true)
                                                        && x.Store.Id == request.StoreId
                                                  )
                                         .OrderByDescending(x => x.CreateOn)
                                         .Select(x => new GetOrderResponse()
                                         {
                                             OrderId = x.Id,
                                             StoreName = x.Store.Name ?? "Unknown",
                                             StoreId = x.Store.Id,
                                             OrderStatus = x.LK_OrderStatus.HasValue ? x.LK_OrderStatus.Value : Domain.Enums.LK_OrderStatus.None,
                                             TotalPrice = x.ProductDetails.Sum(pd => pd.TotalPrice.GetValueOrDefault()),
                                             UserName = x.User.Name ?? "Unknown",
                                             DeliveryAddress = x.DeliveryAddress ?? "",
                                             ContactPhoneNumber = x.User.PhoneNumber ?? "",
                                             OrderType = (int)x.LK_OrderType.Value,
                                             PaymentMethod = (int)x.LK_PaymentMethod.Value,
                                             PickupTime = x.PickupTimeFrom,
                                             IsApproved = x.IsApproved.HasValue ? x.IsApproved.Value : false,
                                             Products = x.ProductDetails.Select(pd => new GetProductInCartResponse()
                                             {
                                                 Id = pd.Product.Id,
                                                 Name = pd.Product.Name,
                                                 Img = pd.Product.Img,
                                                 Description = pd.Product.Description,
                                                 Price = pd.Product.Price,
                                                 PriceDecreases = (pd.Product.ProductStores.FirstOrDefault(ps => ps.Store.Id == x.Store.Id).PriceDecreases.HasValue 
                                                                   && pd.Product.ProductStores.FirstOrDefault(ps => ps.Store.Id == x.Store.Id).PriceDecreases.Value > 0)
                                                                   ? pd.Product.ProductStores.FirstOrDefault(ps => ps.Store.Id == x.Store.Id).PriceDecreases.Value
                                                                   : pd.Product.PriceDecreases,
                                                     LK_ProductUnit = pd.Product.LK_ProductUnit,
                                                 CategoryId = pd.Product.Category != null ? pd.Product.Category.Id : 0,
                                                 Quantity = pd.Quantity.HasValue ? pd.Quantity.Value : 0,
                                             })
                                         })
                                         .ToPagedListAsync(request.PageNo, request.PageSize);
        }

        
    }
}
