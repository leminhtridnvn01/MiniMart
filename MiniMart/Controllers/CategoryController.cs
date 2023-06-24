using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMart.API.Exceptions;
using MiniMart.API.Services;
using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.DTOs.Categories;
using MiniMart.Domain.DTOs.Products;
using System.Net;

namespace MiniMart.API.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<PagingResult<GetAllCategoryResponse>> GetCategories([FromQuery] GetAllCategoryRequest request)
        {
            return await _categoryService.GetCategories(request);
        }

        [HttpGet("{categoryId:int}/product")]
        [AllowAnonymous]
        public async Task<PagingResult<GetProductResponse>> GetProducts([FromQuery] GetProductRequest request, [FromRoute] int categoryId)
        {
            try
            {
                return await _categoryService.GetProducts(request, categoryId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("{categoryId:int}/product")]
        public async Task<int> AddProductAsyncs([FromQuery]AddProductRequest request, [FromRoute] int categoryId)
        {
            try
            {
                return await _categoryService.AddProductAsyncs(request, categoryId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
    }
}
