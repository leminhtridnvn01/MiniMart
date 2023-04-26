namespace MiniMart.Domain.Entities
{
    public partial class Category
    {
        public void AddProduct(Product product)
        {
            this.Products.Add(product);
        }
    }
}
