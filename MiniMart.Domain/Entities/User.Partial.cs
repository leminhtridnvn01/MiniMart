namespace MiniMart.Domain.Entities
{
    public partial class User
    {
        public void AddFavouriteProduct(Product product, int quantity)
        {
            this.FavouriteProducts.Add(new FavouriteProduct
            {
                Product = product,
                User = this,
                Quantity = quantity
            });
        }

        public void UpdateQuantityFavouriteProduct(Product product, int quantity)
        {
            var favouriteProduct = this.FavouriteProducts.Single(x => x.Product.Id == product.Id);
            favouriteProduct.Quantity += quantity;
        }
    }
}
