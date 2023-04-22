using MiniMart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.Entities
{
    public class District : Entity
    {
        public District()
        {
            Wards = new List<Ward>();
        }
        //
        public string? Name { get; set; }
        //
        public virtual City City { get; set; }
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
