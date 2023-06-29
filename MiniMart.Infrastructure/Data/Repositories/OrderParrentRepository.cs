using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces.Repositories;

namespace MiniMart.Infrastructure.Data.Repositories
{
    public class OrderParrentRepository : GenericRepository<OrderParrent>, IOrderParrentRepository
    {
        public OrderParrentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
