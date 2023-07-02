using Microsoft.AspNetCore.Mvc;
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
        public async Task<PagingResult<GetOrderResponse>> GetOrders([FromQuery] GetOrderRequest request)
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

        [HttpGet("get-waiting-for-pay")]
        public async Task<List<GetOrderParrentResponse>> GetOrdersVer2([FromQuery] GetOrderRequest request)
        {
            try
            {
                return await _orderService.GetOrdersVer2(request);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }

        [HttpGet("get-manage-order")]
        public async Task<PagingResult<GetOrderResponse>> GetMangerOrders([FromQuery] GetManagerOrderRequest request)
        {
            try
            {
                return await _orderService.GetMangerOrders(request);
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

        [HttpPut("edit-pickup-time")]
        public async Task<bool> UpdatePickupTimeAsync(UpdatePickupTimeRequest request)
        {
            try
            {
                return await _orderService.UpdatePickupTimeAsync(request);
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

        [HttpGet("approve-order/{orderId:int}")]
        public async Task<bool> ApproveOrderAsync([FromRoute]int orderId)
        {
            try
            {
                return await _orderService.ApproveOrderAsync(orderId);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }

        [HttpPut("update-order-status")]
        public async Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusRequest request)
        {
            try
            {
                return await _orderService.UpdateOrderStatusAsync(request);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }
    }
}
