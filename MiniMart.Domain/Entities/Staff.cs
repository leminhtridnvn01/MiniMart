using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public class Staff : Entity
    {
        public Staff()
        {

        }
        //
        public string? NationalId { get; set; }
        //
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Store? Store { get; set; }
    }
}
