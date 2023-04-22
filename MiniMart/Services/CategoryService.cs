using Microsoft.EntityFrameworkCore;
using MiniMart.API.Extensions;
using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.DTOs.Categories;
using MiniMart.Domain.Interfaces;
using MiniMart.Domain.Interfaces.Repositories;

namespace MiniMart.API.Services
{
    public class CategoryService : BaseService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork
            , ICategoryRepository categoryRepository) : base(unitOfWork)
        {
            _categoryRepository = categoryRepository;
        }
        
        public async Task<PagingResult<GetAllCategoryResponse>> GetCategories(GetAllCategoryRequest request)
        {
            var categoriesQuery = _categoryRepository.GetQuery().Select(new GetAllCategoryResponse().GetSelection());
            return await categoriesQuery.ToPagedListAsync(request.PageNo, request.PageSize);
        }
    }
}
