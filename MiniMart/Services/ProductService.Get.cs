using Microsoft.EntityFrameworkCore;
using MiniMart.Domain.DTOs.Products;

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

    }
}
