using MiniMart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.Entities
{
    public partial class Payment : Entity
    {
        public Payment()
        {

        }
        public int? OrderParrentId { get; set; }
        public int UserId { get; set; }
        public bool? Status { get; set; }
        public int Amount { get; set; }
        public string? TranCode { get; set; }
        public string? PaymentCode { get; set; }
        public string OrderDesc { get; set; }
        //
        public virtual OrderParrent? OrderParrent { get; set; }
    }
}
