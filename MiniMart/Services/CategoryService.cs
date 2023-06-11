using MiniMart.API.Exceptions;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces;
using MiniMart.Domain.Interfaces.Repositories;
using MiniMart.Infrastructure.Data.Repositories;
using System.Net;

namespace MiniMart.API.Services
{
    public partial class CategoryService : BaseService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductStoreRepository _productStoreRepository;

        public CategoryService(IUnitOfWork unitOfWork
            , ICategoryRepository categoryRepository
            , IProductRepository productRepository
            , IProductStoreRepository productStoreRepository
        ) : base(unitOfWork )
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productStoreRepository = productStoreRepository;
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
    }
}
