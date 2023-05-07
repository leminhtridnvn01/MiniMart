using MiniMart.API.Exceptions;
using MiniMart.Domain.DTOs.Products;
using System.Net;

namespace MiniMart.API.Services
{
    public partial class ProductService
    {
        public async Task<bool> AddProduct(AddProductStoreRequest request)
        {
            var product = await ValidateProduct(request.ProductId);
            var store = await ValidateStore(request.StoreId);
            var isProductStoreExited = await _productStoreRepository.AnyAsync(x => x.Product.Id == product.Id && x.Store.Id == store.Id);
            if(isProductStoreExited)
            {
                store.AddQuantityProduct(product, request.Quantity);
            }
            else
            {
                store.AddProduct(product, request.Quantity);
            }
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
