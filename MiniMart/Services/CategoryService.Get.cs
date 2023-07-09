using Azure.Storage.Sas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var products = await _productRepository.GetQuery(x => x.Category.Id == category.Id 
                                                                    && (!request.IsSale.HasValue
                                                                      || (x.PriceDecreases.HasValue && x.PriceDecreases > 0)
                                                                      || x.ProductStores.Any(ps => ps.PriceDecreases.HasValue && ps.PriceDecreases > 0)
                                                                    ))
                                                   .Select(new GetProductResponse().GetSelection())
                                                   .ToPagedListAsync(request.PageNo, request.PageSize);
            foreach(var product in products.Data)
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
                var imgUri = await GetSasUriAsync(product.Img, BlobSasPermissions.Read, new DateTimeOffset(DateTime.UtcNow.AddDays(1)));
                product.Img = imgUri.ToString();
            }
            return products;
        }

        public async Task<PagingResult<GetProductResponse>> GetProductsVer2([FromQuery] GetProductRequest request, [FromRoute] int categoryId)
        {
            var category = await ValidateCategory(categoryId);
            var products = await _productStoreRepository.GetQuery(ps => ps.Product.Category.Id == category.Id
                                                                        && (!request.IsSale.HasValue
                                                                           || (ps.Product.PriceDecreases.HasValue && ps.Product.PriceDecreases > 0)
                                                                        ))
                                                        .Select(ps => new GetProductResponse()
                                                        {
                                                            Id = ps.Product.Id,
                                                            Name = ps.Product.Name,
                                                            Img = ps.Product.Img,
                                                            Description = ps.Product.Description,
                                                            Price = ps.Product.Price,
                                                            PriceDecreases = ps.Product.PriceDecreases,
                                                            LK_ProductUnit = ps.Product.LK_ProductUnit,
                                                            CategoryId = ps.Product.Category != null ? ps.Product.Category.Id : 0,
                                                            StoreId = ps.Store.Id,
                                                            StoreName = ps.Store.Name,
                                                            CityId = ps.Store.Ward.District.City.Id,
                                                            CityName = ps.Store.Ward.District.City.Name
                                                        })
                                                        .ToPagedListAsync(request.PageNo, request.PageSize);

            return products;
        }

        public async Task<Uri> GetSasUriAsync(string fileName, BlobSasPermissions permissions, DateTimeOffset expiresOn)
        {
            var blob = _azureBlobClient.GetBlobClient(fileName);
            return blob.GenerateSasUri(permissions, expiresOn);
        }
    }
}
