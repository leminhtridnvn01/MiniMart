using Microsoft.AspNetCore.Mvc;
using MiniMart.API.Services;
using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.DTOs.Categories;

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
        public async Task<PagingResult<GetAllCategoryResponse>> GetCategories([FromQuery] GetAllCategoryRequest request)
        {
            return await _categoryService.GetCategories(request);
        }
    }
}
