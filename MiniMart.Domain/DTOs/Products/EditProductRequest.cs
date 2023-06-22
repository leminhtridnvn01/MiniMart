using MiniMart.Domain.Enums;

namespace MiniMart.Domain.DTOs.Products
{
    public class EditProductToOrderRequest
    {
        public EditProductToOrderRequest()
        {

        }
        //
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Img { get; set; }
        public int? Price { get; set; }
        public int? PriceDecreases { get; set; }
        public int? Quantity { get; set; }
        public LK_ProductUnit? LK_ProductUnit { get; set; }
        public int CategoryId { get; set; }
        public int StoreId { get; set; } 
    }
}
