using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.DTOs.Orders
{
    public class OrderProcessResponse
    {
        public OrderProcessResponse()
        {

        }
        public bool? IsHasError { get; set; }
        public string? Url { get; set; }
    }
}
