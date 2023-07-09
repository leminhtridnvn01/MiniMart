namespace MiniMart.Domain.DTOs.Strategies
{
    public class AddProductStoreToStrategyRequest
    {
        public AddProductStoreToStrategyRequest()
        {

        }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int StrategyId { get; set; }
    }
}
