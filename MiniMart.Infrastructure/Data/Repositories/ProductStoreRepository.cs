using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces.Repositories;

namespace MiniMart.Infrastructure.Data.Repositories
{
    public class ProductStoreRepository : GenericRepository<ProductStore>, IProductStoreRepository
    {
        public ProductStoreRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
