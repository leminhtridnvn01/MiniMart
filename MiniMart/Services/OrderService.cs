using MiniMart.API.Exceptions;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces;
using MiniMart.Domain.Interfaces.Repositories;
using MiniMart.Infrastructure.Data.Repositories;
using System.Net;
using System.Security.Claims;

namespace MiniMart.API.Services
{
    public partial class OrderService : BaseService
    {
        private readonly ClaimsPrincipal _user;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFavouriteProductRepository _favouriteProductRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IUnitOfWork unitOfWork
            , IOrderRepository orderRepository
            , IProductDetailRepository productDetailRepository
            , IProductRepository productRepository
            , IFavouriteProductRepository favouriteProductRepository
            , IStoreRepository storeRepository
            , ICityRepository cityRepository
            , IUserRepository userRepository
            , ClaimsPrincipal user) : base(unitOfWork)
        {
            _user = user;
            _orderRepository = orderRepository;
            _productDetailRepository = productDetailRepository;
            _productRepository = productRepository;
            _favouriteProductRepository = favouriteProductRepository;
            _storeRepository = storeRepository;
            _cityRepository = cityRepository;
            _userRepository = userRepository;
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
