namespace MiniMart.Domain.DTOs.Strategies
{
    public class AddStrategyRequest
    {
        public AddStrategyRequest()
        {
            Products = new List<AddProductStrategy>();
        }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? PercentageDecrease { get; set; }
        public DateTime? ActivatedDateFrom { get; set; }
        public DateTime? ActivatedDateTo { get; set; }
        public List<AddProductStrategy> Products { get; set; }
    }

    public class AddProductStrategy
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int? PercentageDecreases { get; set; }
    }
}
