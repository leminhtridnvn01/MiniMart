using MiniMart.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.DTOs.Orders
{
    public class OrderInfo
    {
        public OrderInfo()
        {

        }
        public int OrderId { get; set; }
        public int? Amount { get; set; }
        public string? OrderDesc { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string? Status { get; set; }

        public int? PaymentTranId { get; set; }
        public string? BankCode { get; set; }
        public string? PayStatus { get; set; }

    }
}
