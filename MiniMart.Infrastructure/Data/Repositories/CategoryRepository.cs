using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces.Repositories;

namespace MiniMart.Infrastructure.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
