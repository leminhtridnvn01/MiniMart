namespace MiniMart.Domain.Entities
{
    public partial class Store
    {
        public void AddProduct(Product product, int? quantity)
        {
            this.ProductStores.Add(new ProductStore
            {
                Store = this,
                Product = product,
                Quantity = quantity,
            });
        }

        public void AddQuantityProduct(Product product, int? quantity)
        {
            var productStore = this.ProductStores.Single(x => x.Product.Id == product.Id && x.Store.Id == this.Id);
            productStore.Quantity += quantity;
        }
    }
}
