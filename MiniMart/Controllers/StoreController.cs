﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMart.API.Services;
using MiniMart.Domain.DTOs.Orders;
using MiniMart.Domain.DTOs.Stores;

namespace MiniMart.API.Controllers
{
    public class StoreController : BaseController
    {
        private readonly StoreService _storeService;

        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("{cityId:int}")]
        [AllowAnonymous]
        public async Task<List<GetStoreLocationResponse>> GetStoreLocations([FromRoute] int cityId)
        {
            try
            {
                return await _storeService.GetStoreLocations(cityId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet("get-store-manager")]
        public async Task<List<GetLocationManageStoreResponse>> GetMyStoreLocations()
        {
            try
            {
                return await _storeService.GetMyStoreLocations();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet("get-revenue")]
        public async Task<GetRenueveResponse> GetRevenueOrderAsync([FromQuery] GetRenueveRequest request)
        {
            try
            {
                return await _storeService.GetRevenueOrderAsync(request);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
