using MediatR;
using MiniMart.API.Exceptions;
using MiniMart.API.Extensions;
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
            if (isProductStoreExited)
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

        public async Task<bool> AddProductToCart(AddProductToCartRequest request)
        {
            var user = await ValidateUser(_user.GetUserId());
            var product = await ValidateProduct(request.ProductId);
            var isFavouriteProductExisted = await _favouriteProductRepository.AnyAsync(x => x.Product.Id == product.Id && x.User.Id == user.Id);
            if (isFavouriteProductExisted)
            {
                user.UpdateQuantityFavouriteProduct(product, request.Quantity);
            }
            else
            {
                user.AddFavouriteProduct(product, request.Quantity);
            }
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

    }
}
