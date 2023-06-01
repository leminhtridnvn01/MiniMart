using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMart.API.Services;
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
    }
}
