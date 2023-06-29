using MiniMart.Domain.Base.BaseDTOs;

namespace MiniMart.Domain.DTOs.Orders
{
    public class GetRenueveRequest : PagingRequestBase
    {
        public GetRenueveRequest()
        {
            
        }

        public int StoreId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}