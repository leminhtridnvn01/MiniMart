using MiniMart.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.DTOs.Orders
{
    public class UpdatePaymentMethodRequest
    {
        public UpdatePaymentMethodRequest()
        {

        }

        public int OrderId { get; set; }
        public LK_PaymentMethod LK_PaymentMethod { get; set; }
    }
}
