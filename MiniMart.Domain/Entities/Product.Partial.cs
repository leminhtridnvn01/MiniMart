using MiniMart.Domain.Enums;

namespace MiniMart.Domain.Entities
{
    public partial class Product
    {
        public void Update(string name, string description, int? price, int? priceDecreases, Category category, LK_ProductUnit lK_ProductUnit, string img, int? quantity, int storeId)
        {
            Name = name;
            Description = description;
            Price = price ?? Price;
            PriceDecreases = priceDecreases ?? PriceDecreases;
            Category = category;
            LK_ProductUnit = lK_ProductUnit;
            Img = String.IsNullOrEmpty(img) ? Img : img;
            if(quantity.HasValue){
                this.ProductStores.FirstOrDefault(ps => ps.Store.Id == storeId && ps.Product.Id == this.Id).Quantity = quantity.Value;
            }
        }
    }
}
