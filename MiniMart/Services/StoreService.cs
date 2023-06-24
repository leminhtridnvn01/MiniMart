using MiniMart.API.Exceptions;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces;
using MiniMart.Domain.Interfaces.Repositories;
using System.Net;
using System.Security.Claims;

namespace MiniMart.API.Services
{
    public partial class StoreService : BaseService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ClaimsPrincipal _user;

        public StoreService(IUnitOfWork unitOfWork
            , IStoreRepository storeRepository
            , IOrderRepository orderRepository
            , ClaimsPrincipal user) : base(unitOfWork)
        {
            _storeRepository = storeRepository;
            _orderRepository = orderRepository;
            _user = user;
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
