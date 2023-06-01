using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces.Repositories;

namespace MiniMart.Infrastructure.Data.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
