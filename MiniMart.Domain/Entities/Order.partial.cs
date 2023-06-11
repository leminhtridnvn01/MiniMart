namespace MiniMart.Domain.Entities
{
    public partial class Order
    {
        public void AddOrder(IEnumerable<Product> products)
        {
        }

        public void UpdateDeliveryInfo(string userName, string address, string phoneNumber)
        {
            this.UserName = userName;
            this.DeliveryAddress = address;
            this.PhoneNumber = phoneNumber;
        }
    }
}
