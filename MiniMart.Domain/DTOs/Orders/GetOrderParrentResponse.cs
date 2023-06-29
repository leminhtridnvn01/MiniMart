using MiniMart.Domain.DTOs.Products;
using MiniMart.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.DTOs.Orders
{
    public class GetOrderParrentResponse
    {
        public GetOrderParrentResponse()
        {

        }
        public int OrderParrentId { get; set; }
        public LK_OrderStatus OrderStatus { get; set; }
        public int? PaymentMethod { get; set; }
        public int? TotalPrice { get; set; }
        public IEnumerable<GetOrderResponse> Orders { get; set; }
    }
}
