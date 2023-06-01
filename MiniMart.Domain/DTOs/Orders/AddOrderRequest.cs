using MiniMart.Domain.DTOs.Products;
using MiniMart.Domain.Enums;

namespace MiniMart.Domain.DTOs.Orders
{
    public class AddOrderRequest
    {
        public AddOrderRequest()
        {

        }

        public int CityId { get; set; }
        public int StoreId { get; set; }
        public LK_OrderType OrderType { get; set; }
        public DateTime? PickupTimeFrom { get; set; }
        public DateTime? PickupTimeTo { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public List<AddProductToOrderRequest> Products { get; set; }
    }
}
