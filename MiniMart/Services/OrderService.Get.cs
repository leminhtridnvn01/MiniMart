﻿using Microsoft.EntityFrameworkCore;
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
            return await _orderRepository.GetQuery(x => request.OrderStatus.HasValue 
                                                        ? x.LK_OrderStatus.Value == request.OrderStatus.Value
                                                        : true)
                                         .Select(x => new GetOrderResponse()
                                         {
                                             OrderId = x.Id,
                                             StoreName = x.Store.Name ?? "Unknown",
                                             OrderStatus = x.LK_OrderStatus.HasValue ? x.LK_OrderStatus.Value : Domain.Enums.LK_OrderStatus.None,
                                             Products = x.ProductDetails.Select(pd => new GetProductInCartResponse()
                                             {
                                                 Id = pd.Product.Id,
                                                 Name = pd.Product.Name,
                                                 Img = pd.Product.Img,
                                                 Description = pd.Product.Description,
                                                 Price = pd.Product.Price,
                                                 PriceDecreases = pd.Product.PriceDecreases,
                                                 LK_ProductUnit = pd.Product.LK_ProductUnit,
                                                 CategoryId = pd.Product.Category != null ? pd.Product.Category.Id : 0,
                                                 Quantity = pd.Quantity.HasValue ? pd.Quantity.Value : 0
                                             })
                                         })
                                         .ToPagedListAsync(request.PageNo, request.PageSize);
        }
    }
}
