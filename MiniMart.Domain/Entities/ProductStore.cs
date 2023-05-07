using MiniMart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.Entities
{
    public class ProductStore : Entity
    {
        public ProductStore()
        {
            Quantity = 0;
        }
        //
        public int? Quantity { get; set; }
        //
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }

    }
}
