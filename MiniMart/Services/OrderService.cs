using Azure.Storage.Blobs;
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
        private readonly IOrderParrentRepository _orderParrentRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFavouriteProductRepository _favouriteProductRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductStoreRepository _productStoreRepository;
        private readonly PaymentService _paymentService;
        private readonly BlobContainerClient _azureBlobClient;

        public OrderService(IUnitOfWork unitOfWork
            , IOrderRepository orderRepository
            , IOrderParrentRepository orderParrentRepository
            , IProductDetailRepository productDetailRepository
            , IProductRepository productRepository
            , IFavouriteProductRepository favouriteProductRepository
            , IStoreRepository storeRepository
            , ICityRepository cityRepository
            , IUserRepository userRepository
            , IProductStoreRepository productStoreRepository
            , PaymentService paymentService
            , IConfiguration configuration
            , ClaimsPrincipal user) : base(unitOfWork)
        {
            _user = user;
            _orderRepository = orderRepository;
            _orderParrentRepository = orderParrentRepository;
            _productDetailRepository = productDetailRepository;
            _productRepository = productRepository;
            _favouriteProductRepository = favouriteProductRepository;
            _storeRepository = storeRepository;
            _cityRepository = cityRepository;
            _userRepository = userRepository;
            _productStoreRepository = productStoreRepository;
            _paymentService = paymentService;
            _azureBlobClient = new BlobContainerClient(configuration.GetSection("BlobStorageConnectionString").Value, configuration.GetSection("BlobStorageContainerName").Value);
        }

        private async Task<Order> ValidateOrder(int orderId)
        {
            var order = await _orderRepository.GetAsync(x => x.Id == orderId);
            if (order == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the Order with Id equal " + orderId);
            }
            return order;
        }

        private async Task<OrderParrent> ValidateOrderParrent(int orderParrentId)
        {
            var orderParrent = await _orderParrentRepository.GetAsync(x => x.Id == orderParrentId);
            if (orderParrent == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the OrderParrent with Id equal " + orderParrentId);
            }
            return orderParrent;
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
        private async Task<Product> ValidateProduct(int productId)
        {
            var product = await _productRepository.GetAsync(x => x.Id == productId);
            if (product == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the Product with Id equal " + productId);
            }
            return product;
        }

        private async Task<(Product, Store)> ValidateProductInStore(int productId, int storeId)
        {
            var product = await _productRepository.GetAsync(x => x.Id == productId);
            if (product == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the Product with Id equal " + productId);
            }
            var store = await _storeRepository.GetAsync(x => x.Id == storeId);
            if (store == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the Store with Id equal " + storeId);
            }
            var isValid = await _productStoreRepository.AnyAsync(x => x.Store.Id == store.Id && x.Product.Id == product.Id && x.Quantity > 0);
            if (!isValid)
            {
                throw new HttpException(HttpStatusCode.BadRequest, "This product is currently out of stock at this store.");
            }
            return (product, store);
        }

        private async Task<Product> ValidateProduct(int productId, int storeId)
        {
            var product = await _productRepository.GetAsync(x => x.Id == productId);
            if (product == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the Product with Id equal " + productId);
            }
            var store = await _storeRepository.GetAsync(x => x.Id == storeId);
            if (store == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the Store with Id equal " + storeId);
            }
            var isValid = await _productStoreRepository.AnyAsync(x => x.Store.Id == store.Id && x.Product.Id == product.Id && x.Quantity > 0);
            if (!isValid)
            {
                throw new HttpException(HttpStatusCode.BadRequest, "This product is currently out of stock at this store.");
            }
            return product;
        }
    }
}
