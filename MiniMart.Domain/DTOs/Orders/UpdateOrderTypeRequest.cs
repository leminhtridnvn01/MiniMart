using MiniMart.Domain.Enums;

namespace MiniMart.Domain.DTOs.Orders
{
    public class UpdateOrderTypeRequest
    {
        public UpdateOrderTypeRequest()
        {

        }
        public int OrderId { get; set; }
        public LK_OrderType LK_OrderType { get; set; }
    }
}
