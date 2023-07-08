using MiniMart.Domain.Enums;

namespace MiniMart.Domain.DTOs.Strategies
{
    public class GetStrategyResponse
    {
        public GetStrategyResponse()
        {
            
        }
        public int StrategyId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? PercentageDecrease { get; set; }
        public LK_ActivatedStrategyStatus? LK_ActivatedStrategyStatus { get; set; }
        public DateTime? ActivatedDateFrom { get; set; }
        public DateTime? ActivatedDateTo { get; set; }
        public IEnumerable<GetStrategyProductResponse> Products { get; set; }
    }

    public class GetStrategyProductResponse
    {
        public GetStrategyProductResponse()
        {
            
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int? PercentageDecrease { get; set; }
        public int? OriginalPrice { get; set; }
        public int? OriginalPriceDecreases { get; set; }
        public int? CurrentPrice { get; set; }
        public int? CurrentPriceDecreases { get; set; }
    }
}