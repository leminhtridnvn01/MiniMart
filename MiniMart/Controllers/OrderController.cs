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
    }
}
