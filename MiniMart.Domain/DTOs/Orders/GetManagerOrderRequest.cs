using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.Enums;

namespace MiniMart.Domain.DTOs.Orders
{
    public class GetManagerOrderRequest: PagingRequestBase
    {
        public GetManagerOrderRequest()
        {
            
        }

        public int StoreId { get; set; }
        public LK_OrderStatus? OrderStatus { get; set; }
    }
}