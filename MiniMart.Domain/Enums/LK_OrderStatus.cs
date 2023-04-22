using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.Enums
{
    public enum LK_OrderStatus
    {
        None = 0,
        WaitingForPayment = 1,
        WaitingForDelivery = 2,
        DeliveryCancle = 3,
        Complete = 4,
        RejectForPayment = 5,
    }
}
