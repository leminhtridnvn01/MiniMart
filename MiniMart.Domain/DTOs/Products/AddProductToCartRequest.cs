﻿namespace MiniMart.Domain.DTOs.Products
{
    public class AddProductToCartRequest
    {
        public AddProductToCartRequest()
        {

        }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int StoreId { get; set; }
    }
}
