﻿using Microsoft.AspNetCore.Mvc;
using MiniMart.API.Extensions;
using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.DTOs.Categories;
using MiniMart.Domain.DTOs.Products;
using System.Linq;

namespace MiniMart.API.Services
{
    public partial class CategoryService
    {
        public async Task<PagingResult<GetAllCategoryResponse>> GetCategories(GetAllCategoryRequest request)
        {
            var categoriesQuery = _categoryRepository.GetQuery().Select(new GetAllCategoryResponse().GetSelection());
            return await categoriesQuery.ToPagedListAsync(request.PageNo, request.PageSize);
        }
        public async Task<PagingResult<GetProductResponse>> GetProducts([FromQuery] GetProductRequest request, [FromRoute] int categoryId)
        {
            var category = await ValidateCategory(categoryId);
             var products = await _productRepository.GetQuery(x => x.Category.Id == category.Id)
                                                    .Select(new GetProductResponse().GetSelection())
                                                    .ToPagedListAsync(request.PageNo, request.PageSize);

            return products;
        }
    }
}
