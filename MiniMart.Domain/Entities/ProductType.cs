using MiniMart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.Entities
{
    public partial class ProductType : Entity
    {
        public ProductType()
        {

        }
        //
        public string TypeName { get; set; }
        public int Price { get; set; }
        public int PriceDecrease { get; set; }
        public string Img { get; set; }
        public bool IsDefault { get; set; }
        //
        public virtual  Product Product { get; set; }
    }
}
