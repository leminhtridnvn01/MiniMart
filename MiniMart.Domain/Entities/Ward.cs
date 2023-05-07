using MiniMart.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id {get;set;}
        public string? Name { get; set; }
        //
        public virtual District District { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
