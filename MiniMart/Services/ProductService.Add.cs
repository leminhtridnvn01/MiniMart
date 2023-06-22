using MediatR;
using MiniMart.API.Exceptions;
using MiniMart.API.Extensions;
using MiniMart.Domain.DTOs.Products;
using MiniMart.Domain.Entities;
using System.Net;

namespace MiniMart.API.Services
{
    public partial class ProductService
    {
        public async Task<bool> CreateProduct(CreateProductToOrderRequest request)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var category = await ValidateCategory(request.CategoryId);

                var product = new Product()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Category = category,
                    LK_ProductUnit = request.LK_ProductUnit,
                    Price = request.Price,
                    PriceDecreases = request.PriceDecreases,
                };

                await _productRepository.InsertAsync(product);
                await _unitOfWork.SaveChangeAsync();

                foreach (var storeId in request.StoreIds)
                {
                    var store = await ValidateStore(storeId);
                    store.AddProduct(product, null);
                }

                await _unitOfWork.SaveChangeAsync();
                return await _unitOfWork.CommitTransaction();;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }

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
            var (product, store) = await ValidateProductInStore(request.ProductId, request.StoreId);
            var isFavouriteProductExisted = await _favouriteProductRepository.AnyAsync(x => x.Product.Id == product.Id && x.Store.Id == store.Id && x.User.Id == user.Id);
            if (isFavouriteProductExisted)
            {
                user.UpdateQuantityFavouriteProduct(product, store, request.Quantity);
            }
            else
            {
                user.AddFavouriteProduct(product, store, request.Quantity);
            }
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<bool> EditProduct(EditProductToOrderRequest request)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var category = await ValidateCategory(request.CategoryId);
                var (product, store) = await ValidateProductStore(request.ProductId, request.StoreId);

                product.Update(request.Name, request.Description, request.Price, request.PriceDecreases, category, request.LK_ProductUnit.Value, request.Img, request.Quantity, request.StoreId);

                await _unitOfWork.SaveChangeAsync();
                return await _unitOfWork.CommitTransaction();;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }
    }
}
