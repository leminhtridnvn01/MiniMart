using Microsoft.EntityFrameworkCore;
using MiniMart.API.Exceptions;
using MiniMart.API.Extensions;
using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.DTOs.Orders;
using MiniMart.Domain.DTOs.Products;
using MiniMart.Domain.DTOs.Stores;
using MiniMart.Domain.Entities;
using System.Net;

namespace MiniMart.API.Services
{
    public partial class StoreService
    {
        public async Task<List<GetStoreLocationResponse>> GetStoreLocations(int cityId)
        {
            return await _storeRepository.GetQuery(x => x.Ward.District.City.Id == cityId)
                                         .Select(new GetStoreLocationResponse().GetSelection())
                                         .ToListAsync();
        }

        public async Task<List<GetLocationManageStoreResponse>> GetMyStoreLocations()
        {
            var user = await _userRepository.GetAsync(x => x.Id == _user.GetUserId());
            if(user == null)
            {
                throw new HttpException(HttpStatusCode.Unauthorized, "Invalid User");
            }
            if(user.Manager != null)
            {
                return await _storeRepository.GetQuery(x => x.Manager.UserId == _user.GetUserId())
                                         .Select(new GetStoreLocationResponse().GetSelection())
                                         .GroupBy(x => x.CityId)
                                         .Select(x => new GetLocationManageStoreResponse()
                                         {
                                             CityId = x.Key,
                                             CityName = x.FirstOrDefault().CityName,
                                             Stores = x.ToList()
                                         })
                                         .ToListAsync();
            }
            else if(user.Staff != null)
            {
                return await _userRepository.GetQuery(x => x.Id == _user.GetUserId())
                                            .Select(x => x.Staff.Store)
                                            .Select(new GetStoreLocationResponse().GetSelection())
                                            .GroupBy(x => x.CityId)
                                            .Select(x => new GetLocationManageStoreResponse()
                                            {
                                                CityId = x.Key,
                                                CityName = x.FirstOrDefault().CityName,
                                                Stores = x.ToList()
                                            })
                                            .ToListAsync();
            }
            return new List<GetLocationManageStoreResponse>();
        }

        public async Task<GetRenueveResponse> GetRevenueOrderAsync(GetRenueveRequest request )
        {
            var store = await ValidateStore(request.StoreId);
            var orderQuery = _orderRepository.GetQuery(o => (!o.IsPaid.HasValue || o.IsPaid.Value)
                                                            && o.Store.Id == store.Id
                                                            && o.LK_OrderStatus == Domain.Enums.LK_OrderStatus.Complete
                                                            && o.CreateOn >= request.StartDate
                                                            && o.CreateOn <= request.EndDate);
                                        
            var order = orderQuery.Select(o => new GetRenueveOrderResponse()
                                         {
                                            OrderId = o.Id,
                                            DeliveryAddress = o.DeliveryAddress,
                                            UserName = o.UserName,
                                            PhoneNumber = o.PhoneNumber,
                                            DeliveryFee = o.DeliveryFee,
                                            OriginalPrice = o.OriginalPrice,
                                            PriceDecreases = o.PriceDecreases,
                                            TotalPrice = o.TotalPrice,
                                            LK_OrderStatus = o.LK_OrderStatus,
                                            LK_OrderType = o.LK_OrderType,
                                            LK_PaymentMethod = o.LK_PaymentMethod,
                                            CreatedOn = o.CreateOn
                                         })
                                  .ToPagedList(request.PageNo, request.PageSize);

            var venueve = new GetRenueveResponse()
            {
                TotalRenueve = orderQuery.Sum(o => o.TotalPrice.HasValue ? o.TotalPrice.Value : 0),
                RenueveOrders = order
            };

            return venueve;
        }
    } 
}
