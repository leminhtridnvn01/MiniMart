﻿using MiniMart.API.Exceptions;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces;
using MiniMart.Domain.Interfaces.Repositories;
using System.Net;
using System.Security.Claims;

namespace MiniMart.API.Services
{
    public partial class ProductService : BaseService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductStoreRepository _productStoreRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IFavouriteProductRepository _favouriteProductRepository;
        private readonly ClaimsPrincipal _user;

        public ProductService(IUnitOfWork unitOfWork
            , IUserRepository userRepository
            , IProductRepository productRepository
            , ICategoryRepository categoryRepository
            , IProductStoreRepository productStoreRepository
            , IStoreRepository storeRepository
            , IFavouriteProductRepository favouriteProductRepository
            , ClaimsPrincipal user) : base(unitOfWork)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productStoreRepository = productStoreRepository;
            _storeRepository = storeRepository;
            _favouriteProductRepository = favouriteProductRepository;
            _user = user;
        }
        private async Task<User> ValidateUser(int userId)
        {
            var user = await _userRepository.GetAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the User with Id equal " + userId);
            }
            return user;
        }
        private async Task<Category> ValidateCategory(int categoryId)
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == categoryId);
            if (category == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the Category with Id equal " + categoryId);
            }
            return category;
        }

        private async Task<Product> ValidateProduct(int categoryId, int productId)
        {
            var category = await ValidateCategory(categoryId);
            var product = await _productRepository.GetAsync(x => x.Id == productId && x.Category.Id == category.Id);
            if (product == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the Product with Id equal " + productId);
            }
            return product;
        }

        private async Task<Product> ValidateProduct(int productId)
        {
            var product = await _productRepository.GetAsync(x => x.Id == productId);
            if (product == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the Product with Id equal " + productId);
            }
            return product;
        }

        private async Task<Store> ValidateStore(int storeId)
        {
            var store = await _storeRepository.GetAsync(x => x.Id == storeId);
            if (store == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the Store with Id equal " + storeId);
            }
            return store;
        }
    }
}
