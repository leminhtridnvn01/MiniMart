﻿using Microsoft.AspNetCore.Mvc;
using MiniMart.API.Services;
using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.DTOs.Locations;
using MiniMart.Domain.DTOs.Orders;
using MiniMart.Domain.DTOs.Stores;

namespace MiniMart.API.Controllers
{
    public class OrderController : BaseController
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<PagingResult<GetOrderResponse>> GetOrders([FromQuery]GetOrderRequest request)
        {
            try
            {
                return await _orderService.GetOrders(request);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }

        [HttpGet("get-location/cities")]
        public async Task<List<CityResponse>> GetCitiesForOrder()
        {
            try
            {
                return await _orderService.GetCitiesForOrder();
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }

        [HttpGet("get-location/cities/{cityId:int}/stores")]
        public async Task<List<GetStoreLocationResponse>> GetStores([FromRoute] int cityId)
        {
            try
            {
                return await _orderService.GetStores(cityId);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }

        [HttpPost("add-to-order")]
        public async Task<bool> AddToOrder( [FromBody] AddOrderRequest request)
        {
            try
            {
                return await _orderService.AddOrder(request);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }

        [HttpPost("process-order")]
        public async Task<OrderProcessResponse> ProcessOrderAsync(OrderInfo request)
        {
            try
            {
                return await _orderService.ProcessOrderAsync(request);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }

        [HttpPut("edit-delivery-info")]
        public async Task<bool> UpdateDeliveryInfoOrderAsync([FromBody] UpdateDeliveryAddressOrderRequest request)
        {
            try
            {
                return await _orderService.UpdateDeliveryInfoOrderAsync(request);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }

        [HttpPut("edit-order-type")]
        public async Task<bool> UpdateOrderTypeAsync(UpdateOrderTypeRequest request)
        {
            try
            {
                return await _orderService.UpdateOrderTypeAsync(request);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }

        [HttpPut("edit-payment-method")]
        public async Task<bool> UpdatePaymentMethodAsync(UpdatePaymentMethodRequest request)
        {
            try
            {
                return await _orderService.UpdatePaymentMethodAsync(request);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }
    }
}
