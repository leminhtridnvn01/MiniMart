using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<PagingResult<GetProductResponse>> GetProducts([FromQuery] GetProductRequest request)
        {
            try
            {
                return await _productService.GetProducts(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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

        [HttpGet("get-product-in-cart")]
        public async Task<PagingResult<GetProductInCartResponse>> GetProductInCart()
        {
            try
            {
                return await _productService.GetProductInCart();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet("get-sale-product")]
        public async Task<PagingResult<GetSaleProductResponse>> GetSaleProducts([FromQuery] GetProductRequest request)
        {
            try
            {
                return await _productService.GetSaleProducts(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("{categoryId:int}/product/get-sale-product")]
        public async Task<PagingResult<GetSaleProductResponse>> GetSaleProducts([FromQuery] GetProductRequest request, [FromRoute] int categoryId)
        {
            try
            {
                return await _productService.GetSaleProducts(request, categoryId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("get-manager-product")]
        public async Task<PagingResult<GetProductManagerResponse>> GetStoreProduct([FromQuery] GetProductManagerRequest request)
        {
            try
            {
                return await _productService.GetStoreProduct(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<bool> CreateProduct(CreateProductToOrderRequest request)
        {
            try
            {
                return await _productService.CreateProduct(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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

        [HttpPost("add-product-to-cart")]
        public async Task<bool> AddProductToCart([FromBody] AddProductToCartRequest request)
        {
            try
            {
                return await _productService.AddProductToCart(request);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

         

        //[HttpPost("{storeId:int}/add-to-order")]
        //public async Task<bool> AddToOrder([FromServices] OrderService orderService, [FromBody] List<AddProductToOrderRequest> request, [FromRoute] int storeId)
        //{
        //    try
        //    {
        //        return await orderService.AddOrder(request, storeId);
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
        //}
        #endregion

        #region Put
        [HttpPut]
        public async Task<bool> EditProduct(EditProductToOrderRequest request)
        {
            try
            {
                return await _productService.EditProduct(request);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        #region Patch

        #endregion

        #region Delete

        #endregion
    }
}
