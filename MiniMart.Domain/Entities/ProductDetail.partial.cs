namespace MiniMart.Domain.Entities
{
    public partial class ProductDetail
    {
        public ProductDetail(Product product, Order order, int quantity)
        {
            this.Product = product;
            this.Order = order;
            this.Quantity = quantity;
            this.OriginalPrice = product.Price ?? 0;
            this.PriceDecreases = product.PriceDecreases ?? 0;
            this.TotalPrice = this.Quantity.Value * this.OriginalPrice.Value;
        }
    }
}
