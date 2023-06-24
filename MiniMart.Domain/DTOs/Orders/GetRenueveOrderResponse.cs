using MiniMart.Domain.Enums;

namespace MiniMart.Domain.DTOs.Orders
{
    public class GetRenueveOrderResponse
    {
        public GetRenueveOrderResponse()
        {
            
        }

        public int OrderId { get; set; }
        public int? OriginalPrice { get; set; }
        public int? PriceDecreases { get; set; }
        public int? DeliveryFee { get; set; }
        public int? TotalPrice { get; set; }
        public LK_OrderStatus? LK_OrderStatus { get; set; }
        public LK_PaymentMethod? LK_PaymentMethod { get; set; }
        public LK_OrderType? LK_OrderType { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string? DeliveryAddress { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}