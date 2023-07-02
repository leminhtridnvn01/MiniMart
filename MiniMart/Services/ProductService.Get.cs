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
            var products = await _productRepository.GetQuery(x => x.Name.ToLower().Contains(request.Search.ToLower())
                                                                  && (!request.IsSale.HasValue 
                                                                      || (x.PriceDecreases.HasValue && x.PriceDecreases > 0)  
                                                                  ))
                                                   .Select(new GetProductResponse().GetSelection())
                                                   .ToPagedListAsync(request.PageNo, request.PageSize);
            foreach (var product in products.Data)
            {
                product.Locations = await _productStoreRepository.GetQuery(x => x.Product.Id == product.Id && x.Quantity > 0)
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
            return products;
        }

        public async Task<PagingResult<GetSaleProductResponse>> GetSaleProducts([FromQuery] GetProductRequest request)
        {
            var products = await _productStoreRepository.GetQuery(ps => (ps.PriceDecreases.HasValue && ps.PriceDecreases.Value > 0)
                                                                         || (ps.Product.PriceDecreases.HasValue && ps.Product.PriceDecreases.Value > 0))
                                                        .Select(new GetSaleProductResponse().GetSelection())
                                                        .ToPagedListAsync(request.PageNo, request.PageSize);
            return products;
        }

        public async Task<PagingResult<GetSaleProductResponse>> GetSaleProducts([FromQuery] GetProductRequest request, [FromRoute] int categoryId)
        {
            var category = await ValidateCategory(categoryId);
            var products = await _productStoreRepository.GetQuery(ps => ps.Product.Category.Id == category.Id
                                                                        && (ps.PriceDecreases.HasValue && ps.PriceDecreases.Value > 0)
                                                                        || (ps.Product.PriceDecreases.HasValue && ps.Product.PriceDecreases.Value > 0))
                                                        .Select(new GetSaleProductResponse().GetSelection())
                                                        .ToPagedListAsync(request.PageNo, request.PageSize);
            return products;
        }

        public async Task<PagingResult<GetProductManagerResponse>> GetStoreProduct(GetProductManagerRequest request)
        {
            var store = await ValidateStore(request.StoreId);
            var products = await _productStoreRepository.GetQuery(ps => ps.Store.Id == store.Id
                                                                        && (request.Search.IsNullOrEmpty() 
                                                                            || ps.Product.Id.ToString() == request.Search
                                                                            || ps.Product.Name.Contains(request.Search)
                                                                        ))
                                                        .OrderByDescending(ps => ps.CreateOn)
                                                        .Select(ps => new GetProductManagerResponse()
                                                        {
                                                            ProductId = ps.Product.Id,
                                                            OriginalPrice = ps.Product.Price,
                                                            OriginalPriceDecreases = ps.Product.PriceDecreases,
                                                            CurrentPrice = ps.PriceDecreases,
                                                            CurrentPriceDecreases = ps.PriceDecreases,
                                                            Name = ps.Product.Name ?? "",
                                                            Description = ps.Product.Description ?? "",
                                                            Quantity = ps.Quantity.HasValue ? ps.Quantity.Value : 0,
                                                            CategoryId = ps.Product.Category.Id,
                                                            CategoryName = ps.Product.Category.Name,
                                                            LK_ProductUnit = ps.Product.LK_ProductUnit
                                                        })
                                                        .ToPagedListAsync(request.PageNo, request.PageSize);
            return products;
        }
    }
}
