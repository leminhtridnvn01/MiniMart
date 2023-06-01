namespace MiniMart.Domain.DTOs.Products
{
    public class AddProductToOrderRequest
    {
        public AddProductToOrderRequest()
        {

        }
        //
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
