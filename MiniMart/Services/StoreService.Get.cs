using Microsoft.EntityFrameworkCore;
using MiniMart.Domain.DTOs.Stores;

namespace MiniMart.API.Services
{
    public partial class StoreService
    {
        public async Task<List<GetStoreLocationResponse>> GetStoreLocations(int cityId)
        {
            return await _storeRepository.GetQuery(x => x.Ward.District.City.Id == cityId)
                                         .Select(new GetStoreLocationResponse().GetSelection())
                                         .ToListAsync();
        }
    } 
}
