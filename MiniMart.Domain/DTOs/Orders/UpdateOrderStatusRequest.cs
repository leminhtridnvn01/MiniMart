using MiniMart.Domain.Enums;

namespace MiniMart.Domain.DTOs.Orders
{
    public class UpdateOrderStatusRequest
    {
        public UpdateOrderStatusRequest()
        {

        }
        public int OrderId { get; set; }
        public LK_OrderStatus OrderStatus { get; set; }
    }
}
