﻿using Microsoft.AspNetCore.Mvc;
using MiniMart.API.Services;
using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.DTOs.Locations;
using MiniMart.Domain.DTOs.Products;

namespace MiniMart.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        #region Get
        [HttpGet("/api/Category/{categoryId:int}/product/{productId:int}")]
        public async Task<GetProductResponse> GetProduct([FromRoute] int categoryId, [FromRoute] int productId)
        {
            try
            {
                return await _productService.GetProduct(categoryId, productId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("{productId:int}/get-current-location")]
        public async Task<List<GetProductLocationResponse>> GetLocation([FromRoute] int productId, [FromQuery] GetProductLocationRequest request)
        {
            try
            {
                return await _productService.GetLocation(productId, request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Post
        [HttpPost("add-to-store")]
        public async Task<bool> AddProduct([FromBody] AddProductStoreRequest request)
        {
            try
            {
                return await _productService.AddProduct(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Put

        #endregion

        #region Patch

        #endregion

        #region Delete

        #endregion
    }
}