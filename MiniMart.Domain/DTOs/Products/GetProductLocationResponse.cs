using MiniMart.Domain.DTOs.Locations;
using MiniMart.Domain.Entities;
using System.Linq.Expressions;

namespace MiniMart.Domain.DTOs.Products
{
    public class GetProductLocationResponse
    {
        public GetProductLocationResponse()
        {
            Stores = new List<StoreResponse>();
            CityName = "";
        }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public IEnumerable<StoreResponse> Stores { get; set; }
    }
    public class Test
    {
        public int Id { get; set; }
    }
    public class StoreResponse
    {
        public StoreResponse()
        {

        }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public Expression<Func<ProductStore, StoreResponse>> GetSelection()
        {
            return _ => new StoreResponse()
            {
                CityId = _.Store.Ward.District.City.Id,
                CityName = _.Store.Ward.District.City.Name,
                StoreId = _.Store.Id,
                StoreName = _.Store.Name,
                Address = _.Store.Address + ", " + _.Store.Ward.Name + ", " + _.Store.Ward.District.Name + ", " + _.Store.Ward.District.City.Name,
                ProductId = _.Product.Id,
                ProductName = _.Product.Name,
                Quantity = _.Quantity.Value
            };
        }
    }
}
