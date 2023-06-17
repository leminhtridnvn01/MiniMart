using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces.Repositories;

namespace MiniMart.Infrastructure.Data.Repositories
{
    public class StrategyRepository : GenericRepository<Strategy>, IStrategyRepository
    {
        public StrategyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
