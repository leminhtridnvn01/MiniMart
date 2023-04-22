using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public class ProductDetail : Entity
    {
        public ProductDetail()
        {

        }
        //
        public int? OriginalPrice { get; set; }
        public int? PriceDecreases { get; set; }
        public int? TotalPrice { get; set; }
        public int? Quantity { get; set; }
        //
        public virtual Product Product { get; set; }
        public virtual Order? Order { get; set; }
    }
}
