using MiniMart.Domain.Base.BaseDTOs;

namespace MiniMart.Domain.DTOs.Strategies
{
    public class GetStrategyRequest : PagingRequestBase
    {
        public GetStrategyRequest()
        {
            
        }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}