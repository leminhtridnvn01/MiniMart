using MiniMart.Domain.Base.BaseDTOs;

namespace MiniMart.Domain.DTOs.Orders
{
    public class GetRenueveResponse
    {
        public GetRenueveResponse()
        {
            
        }

        public int TotalRenueve { get; set; }
        public PagingResult<GetRenueveOrderResponse> RenueveOrders { get; set; }
    }
}