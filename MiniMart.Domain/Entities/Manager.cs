using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public class Manager : Entity
    {
        public Manager()
        {
            Stores = new List<Store>();
        }
        //
        public string? NationalID { get; set; }
        //
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
