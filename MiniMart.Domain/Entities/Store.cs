using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public class Store : Entity
    {
        public Store()
        {
            Staffs = new List<Staff>();
            Stores = new List<Store>();
        }
        //
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Address { get; set; }
        //
        public virtual ICollection<Staff> Staffs { get; set;}
        public virtual Ward? Ward { get; set; }
        public virtual ICollection<Store> Stores { get;}
    }
}
