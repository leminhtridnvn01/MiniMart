using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMart.API.Extensions;
using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.DTOs.Products;
using System.Linq;

namespace MiniMart.API.Services
{
    public partial class ProductService
    {
        public async Task<GetProductResponse> GetProduct(int categoryId, int productId)
        {
            var product = await ValidateProduct(categoryId, productId);
            return new GetProductResponse().GetMap(product);
        }

        public async Task<List<GetProductLocationResponse>> GetLocation(int productId, GetProductLocationRequest request)
        {
           return await _productStoreRepository.GetQuery(x => x.Product.Id == productId)
                                                          .Select(new StoreResponse().GetSelection())
                                                          .GroupBy(x => x.CityId)
                                                          .Select(x => new GetProductLocationResponse
                                                          {
                                                              CityId = x.Key,
                                                              CityName = x.FirstOrDefault().CityName,
                                                              Stores = x.ToList()
                                                          })
                                                          .ToListAsync();
            
        }

        public async Task<PagingResult<GetProductInCartResponse>> GetProductInCart()
        {
            var user = await ValidateUser(_user.GetUserId());
            return await _favouriteProductRepository.GetQuery(x => x.User.Id == user.Id)
                                                            .OrderByDescending(x => x.UpdateOn)
                                                            .Select(new GetProductInCartResponse().GetSelection())
                                                            .ToPagedListAsync(1, 9000);
        }
        public async Task<PagingResult<GetProductResponse>> GetProducts([FromQuery] GetProductRequest request)
        {
            if (request.Search.IsNullOrEmpty())
            {
                return new PagingResult<GetProductResponse>();
            }
            var products = await _productRepository.GetQuery(x => x.Name.ToLower().Contains(request.Search.ToLower()))
                                                   .Select(new GetProductResponse().GetSelection())
                                                   .ToPagedListAsync(request.PageNo, request.PageSize);
            return products;
        }
    }
}
