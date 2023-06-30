using MiniMart.Domain.Enums;

namespace MiniMart.Domain.DTOs.Orders
{
    public class UpdatePickupTimeRequest
    {
        public UpdatePickupTimeRequest()
        {

        }
        public int OrderId { get; set; }
        public DateTime PickupTime { get; set; }
    }
}
