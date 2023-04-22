using MiniMart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.Entities
{
    public class Ward : Entity
    {
        public Ward()
        {
            Stores = new List<Store>();
        }
        //
        public string? Name { get; set; }
        //
        public virtual District District { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
