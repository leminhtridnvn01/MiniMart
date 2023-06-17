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
            this.PriceDecreases = (product.ProductStores.FirstOrDefault(ps => ps.Store.Id == order.Store.Id).PriceDecreases.HasValue
                                   && product.ProductStores.FirstOrDefault(ps => ps.Store.Id == order.Store.Id).PriceDecreases.Value > 0)
                                   ? product.ProductStores.FirstOrDefault(ps => ps.Store.Id == order.Store.Id).PriceDecreases.Value
                                   : product.PriceDecreases ?? 0;
            this.TotalPrice = (this.PriceDecreases != null && this.PriceDecreases != 0)
                              ? this.Quantity.Value * this.PriceDecreases.Value
                              : this.Quantity.Value * this.OriginalPrice.Value;
        }
    }
}
