using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces.Repositories;

namespace MiniMart.Infrastructure.Data.Repositories
{
    public class StrategyDetailRepository : GenericRepository<StrategyDetail>, IStrategyDetailRepository
    {
        public StrategyDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
