﻿using MiniMart.Domain.Base;
using MiniMart.Domain.Enums;

namespace MiniMart.Domain.Entities
{
    public partial class Product : Entity
    {
        public Product()
        {
            ProductStores = new List<ProductStore>();
            ProductDetails = new List<ProductDetail>();
            FavouritesProducts = new List<FavouriteProduct>();
        }
        //
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Img { get; set; }
        public int? Price { get; set; }
        public int? PriceDecreases { get; set; }
        public LK_ProductUnit? LK_ProductUnit { get; set; }
        //
        public virtual Category? Category { get; set; }
        public virtual ICollection<ProductStore> ProductStores { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get;}
        public virtual ICollection<FavouriteProduct> FavouritesProducts { get; set;}
        public virtual ICollection<ProductType> ProductTypes { get;}
        public virtual ICollection<StrategyDetail> StrategyDetails { get; set; }
    }
}
