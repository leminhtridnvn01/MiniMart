using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public class City : Entity
    {
        public City()
        {
            this.Districts = new List<District>();
        }
        // 
        public string? Name { get; set; }
        //
        public virtual ICollection<District> Districts { get; set; }
    }
}
