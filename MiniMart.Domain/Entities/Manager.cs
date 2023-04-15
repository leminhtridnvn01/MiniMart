using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public class Manager : Entity
    {
        public Manager()
        {

        }
        //
        public string? NationalID { get; set; }
        //
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
