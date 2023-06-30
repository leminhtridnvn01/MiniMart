using MiniMart.Domain.DTOs.Products;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Enums;
using System.Linq.Expressions;

namespace MiniMart.Domain.DTOs.Orders
{
    public class GetOrderResponse
    {
        public GetOrderResponse()
        {
            this.Products = new List<GetProductInCartResponse>();
        }

        public int OrderId { get; set; }
        public string StoreName { get; set; }
        public int StoreId { get; set; }
        public LK_OrderStatus OrderStatus { get; set; }
        public int TotalPrice { get; set; }
        public string UserName { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactPhoneNumber { get; set; }
        public DateTime? PickupTime { get; set; }
        public int? OrderType { get; set; }
        public int? PaymentMethod { get; set; }
        public bool? IsApproved { get; set; } = false;
        public IEnumerable<GetProductInCartResponse> Products { get; set; }
    }
}
