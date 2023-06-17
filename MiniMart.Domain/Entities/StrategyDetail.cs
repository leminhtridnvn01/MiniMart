using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public partial class StrategyDetail : Entity
    {
        public StrategyDetail()
        {

        }
        //
        public int StrategyId { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int? PercentageDecreases { get; set; }
        //
        public virtual Strategy Strategy { get; set; }
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
