using MiniMart.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }
        public string? Name { get; set; }
        //
        public virtual City City { get; set; }
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
