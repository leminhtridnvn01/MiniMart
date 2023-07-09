using Microsoft.AspNetCore.Mvc;
using MiniMart.API.Services;
using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.DTOs.Strategies;

namespace MiniMart.API.Controllers
{
    public class StrategyController : BaseController
    {
        private readonly StrategyService _strategyService;

        public StrategyController(StrategyService strategyService)
        {
            _strategyService = strategyService;
        }

        [HttpPost("add-strategy")]
        public async Task<bool> AddStrategyAsync(AddStrategyRequest request)
        {
            try
            {
                return await _strategyService.AddStrategyAsync(request);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }

        [HttpPost("add-product-store-to-strategy")]
        public async Task<bool> AddProductStoreToStrategy(AddProductStoreToStrategyRequest request)
        {
            try
            {
                return await _strategyService.AddProductStoreToStrategy(request);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }

        [HttpGet]
        public async Task<bool> Test()
        {
            return await _strategyService.Test();
        }

        [HttpGet("get-manager-strategy")]
        public async Task<PagingResult<GetStrategyResponse>> GetManagerStrategy([FromQuery] GetStrategyRequest request)
        {
            try
            {
                return await _strategyService.GetManagerStrategy(request);
            }
            catch (Exception e)
            {
                throw e ?? new Exception("An error occured");
            }
        }
    }
}
