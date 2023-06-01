using MiniMart.Domain.Base;
using MiniMart.Domain.Enums;

namespace MiniMart.Domain.Entities
{
    public partial class Order : Entity
    {
        public Order()
        {
            TotalPrice = 0;
            OriginalPrice = 0;
            PriceDecreases = 0;
            ProductDetails = new List<ProductDetail>();
        }
        //
        public int? OriginalPrice { get; set; }
        public int? PriceDecreases { get; set; }
        public int? DeliveryFee { get; set; }
        public int? TotalPrice { get; set; }
        public string? DeliveryAddress { get; set; }
        public bool? IsPaid { get; set; }
        public LK_OrderStatus? LK_OrderStatus { get; set; }
        //
        public virtual User User { get; set; }
        public virtual Store Store { get; set; } 
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }  
    }
}
