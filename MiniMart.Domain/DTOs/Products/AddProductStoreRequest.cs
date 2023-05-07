using MiniMart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.DTOs.Products
{
    public class AddProductStoreRequest
    {
        public AddProductStoreRequest()
        {

        }

        public int ProductId { get; set; }  
        public int StoreId { get; set; }
        public int? Quantity { get; set; }

        public ProductStore GetMap()
        {
            return new ProductStore()
            {
                Quantity = this.Quantity,
                UpdateOn = DateTime.UtcNow,
                CreateOn = DateTime.UtcNow
            };
        }
    }
}
