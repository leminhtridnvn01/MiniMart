using MiniMart.Domain.Base;
using MiniMart.Domain.Enums;

namespace MiniMart.Domain.Entities
{
    public partial class Strategy : Entity
    {
        public Strategy()
        {
            StrategyDetails = new List<StrategyDetail>();
        }
        //
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? PercentageDecrease { get; set; }
        public DateTime? ActivatedDateFrom { get; set; }
        public DateTime? ActivatedDateTo { get; set; }
        public LK_ActivatedStrategyStatus? LK_ActivatedStrategyStatus { get; set; }
        //
        public virtual ICollection<StrategyDetail> StrategyDetails { get; set; }
    }
}
