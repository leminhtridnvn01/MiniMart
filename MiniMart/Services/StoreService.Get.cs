using Microsoft.EntityFrameworkCore;
using MiniMart.API.Extensions;
using MiniMart.Domain.DTOs.Products;
using MiniMart.Domain.DTOs.Stores;
using MiniMart.Domain.Entities;

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

        public async Task<List<GetLocationManageStoreResponse>> GetMyStoreLocations()
        {
            return  await _storeRepository.GetQuery(x => x.Manager.UserId == _user.GetUserId())
                                         .Select(new GetStoreLocationResponse().GetSelection())
                                         .GroupBy(x => x.CityId)
                                         .Select(x => new GetLocationManageStoreResponse()
                                         {
                                             CityId = x.Key,
                                             CityName = x.FirstOrDefault().CityName,
                                             Stores = x.ToList()
                                         })
                                         .ToListAsync();
        }
    } 
}
