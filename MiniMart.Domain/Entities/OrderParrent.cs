using MiniMart.Domain.Base;
using MiniMart.Domain.Enums;

namespace MiniMart.Domain.Entities
{
    public class OrderParrent : Entity
    {
        public OrderParrent()
        {
            TotalPrice = 0;
            OriginalPrice = 0;
            PriceDecreases = 0;
            Orders = new List<Order>();
        }
        //
        public int? OriginalPrice { get; set; }
        public int? PriceDecreases { get; set; }
        public int? DeliveryFee { get; set; }
        public int? TotalPrice { get; set; }
        public bool? IsPaid { get; set; }
        public LK_OrderStatus? LK_OrderStatus { get; set; }
        public LK_PaymentMethod? LK_PaymentMethod { get; set; }
        public LK_OrderType? LK_OrderType { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? PickupTimeFrom { get; set; }
        public DateTime? PickupTimeTo { get; set; }
        //
        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
