using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.Enums;

namespace MiniMart.Domain.DTOs.Orders
{
    public class GetOrderRequest : PagingRequestBase
    {
        public GetOrderRequest()
        {
                
        }

        public LK_OrderStatus? OrderStatus { get; set; }
    }
}
