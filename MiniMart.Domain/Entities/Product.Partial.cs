﻿using MiniMart.Domain.Enums;

namespace MiniMart.Domain.Entities
{
    public partial class Product
    {
        public void Update(string name, string description, int? price, int? priceDecreases, int? currentPriceDecreases, Category category, LK_ProductUnit lK_ProductUnit, int? quantity, int storeId)
        {
            Name = name;
            Description = description;
            Price = price ?? Price;
            PriceDecreases = priceDecreases ?? PriceDecreases;
            Category = category;
            LK_ProductUnit = lK_ProductUnit;
            var store = this.ProductStores.FirstOrDefault(ps => ps.Store.Id == storeId && ps.Product.Id == this.Id);
            if(quantity.HasValue){
                store.Quantity = quantity.Value;
            }
            if (currentPriceDecreases.HasValue)
            {
                store.PriceDecreases = currentPriceDecreases.Value;
            }
        }
    }
}
