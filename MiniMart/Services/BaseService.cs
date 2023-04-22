using MiniMart.Domain.Interfaces;

namespace MiniMart.API.Services
{
    public class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
