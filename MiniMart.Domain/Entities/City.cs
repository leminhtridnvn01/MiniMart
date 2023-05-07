using MiniMart.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMart.Domain.Entities
{
    public class City : Entity
    {
        public City()
        {
            this.Districts = new List<District>();
        }
        // 
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }
        public string? Name { get; set; }
        //
        public virtual ICollection<District> Districts { get; set; }
    }
}
