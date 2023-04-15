using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public class Client : Entity
    {
        public Client() 
        { 
        
        }
        //

        //
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
