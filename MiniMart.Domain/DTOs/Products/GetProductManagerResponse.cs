using MiniMart.Domain.Enums;

namespace MiniMart.Domain.DTOs.Products
{
    public class GetProductManagerResponse
    {
        public GetProductManagerResponse()
        {

        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int? OriginalPrice { get; set; }
        public int? OriginalPriceDecreases { get; set; }
        public int? CurrentPrice { get; set; }
        public int? CurrentPriceDecreases { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public LK_ProductUnit? LK_ProductUnit { get; set; }
    }
}
