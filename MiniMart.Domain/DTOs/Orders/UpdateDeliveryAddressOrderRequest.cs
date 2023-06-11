namespace MiniMart.Domain.DTOs.Orders
{
    public class UpdateDeliveryAddressOrderRequest
    {
        public UpdateDeliveryAddressOrderRequest()
        {

        }
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
