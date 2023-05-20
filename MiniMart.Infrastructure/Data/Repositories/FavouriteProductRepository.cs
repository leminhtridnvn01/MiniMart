using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces.Repositories;

namespace MiniMart.Infrastructure.Data.Repositories
{
    public class FavouriteProductRepository : GenericRepository<FavouriteProduct>, IFavouriteProductRepository
    {
        public FavouriteProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
