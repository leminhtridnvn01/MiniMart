using MiniMart.Domain.Interfaces;
using MiniMart.Domain.Interfaces.Repositories;
using System.Security.Claims;

namespace MiniMart.API.Services
{
    public partial class StoreService : BaseService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ClaimsPrincipal _user;

        public StoreService(IUnitOfWork unitOfWork
            , IStoreRepository storeRepository
            , ClaimsPrincipal user) : base(unitOfWork)
        {
            _storeRepository = storeRepository;
            _user = user;
        }


    }
}
