using MiniMart.Domain.Entities;
using System.Linq.Expressions;

namespace MiniMart.Domain.DTOs.Stores
{
    public class GetStoreLocationResponse
    {
        public GetStoreLocationResponse()
        {

        }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }

        public Expression<Func<Store, GetStoreLocationResponse>> GetSelection()
        {
            return _ => new GetStoreLocationResponse()
            {
                CityId = _.Ward.District.City.Id,
                CityName = _.Ward.District.City.Name,
                StoreId = _.Id,
                StoreName = _.Name,
                Address = _.Address + ", " + _.Ward.Name + ", " + _.Ward.District.Name + ", " + _.Ward.District.City.Name,
            };
        }
    }
}
