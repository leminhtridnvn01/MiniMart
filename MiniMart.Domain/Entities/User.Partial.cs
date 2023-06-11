namespace MiniMart.Domain.Entities
{
    public partial class User
    {
        public void AddFavouriteProduct(Product product, Store store, int quantity)
        {
            this.FavouriteProducts.Add(new FavouriteProduct
            {
                Product = product,
                User = this,
                Quantity = quantity,
                Store = store
            });
        }

        public void UpdateQuantityFavouriteProduct(Product product, Store store, int quantity)
        {
            var favouriteProduct = this.FavouriteProducts.Single(x => x.Product.Id == product.Id && x.Store.Id == store.Id);
            favouriteProduct.Quantity += quantity;
        }
    }
}
