using Microsoft.VisualBasic;
using MiniMart.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.DTOs.Payments
{
    public class PaymentInfoRequest
    {
        public PaymentInfoRequest()
        {
            this.OrderDesc = string.Empty;
            this.Language = LK_LanguageType.English;
            this.Bank = LK_BankType.NCB;
            this.OrderCategory = LK_OrderCategoryType.other;

            this.BillingMobile = string.Empty;
            this.BillingEmail = string.Empty;
            this.BillingFirstName = string.Empty;
            this.BillingLastName = string.Empty;
            this.BillingAddress= string.Empty;
            this.BillingCity = string.Empty;
            this.BillingCountry = string.Empty;

            this.ShippingMobile = string.Empty;
            this.ShippingEmail = string.Empty;
            this.ShippingCustomer = string.Empty;
            this.ShippingAddress = string.Empty;
            this.ShippingCompany = string.Empty;
            this.ShippingTaxCode = string.Empty;
            this.ShippingBillType = BillType.I;
        }
        public int OrderParrentId { get; set; }
        public int Amount { get; set; }
        public string OrderDesc { get; set; }
        public LK_LanguageType Language { get; set; }
        public LK_BankType Bank { get; set; }
        public LK_OrderCategoryType OrderCategory { get; set; }

        public string BillingMobile { get; set; }
        public string BillingEmail { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingCountry { get; set; }

        public string ShippingMobile { get; set; }
        public string ShippingEmail { get; set; }
        public string ShippingCustomer { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCompany { get; set; }
        public string ShippingTaxCode { get; set; }
        public BillType ShippingBillType { get; set; }
    }
}
