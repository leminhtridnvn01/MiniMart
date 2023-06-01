using MiniMart.Domain.DTOs.Products;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Enums;
using System.Linq.Expressions;

namespace MiniMart.Domain.DTOs.Orders
{
    public class GetOrderResponse
    {
        public GetOrderResponse()
        {
            this.Products = new List<GetProductInCartResponse>();
        }

        public int OrderId { get; set; }
        public string StoreName { get; set; }
        public LK_OrderStatus OrderStatus { get; set; }
        public IEnumerable<GetProductInCartResponse> Products { get; set; }
    }
}
