using MiniMart.Domain.Base;
using MiniMart.Domain.Enums;

namespace MiniMart.Domain.Entities
{
    public class User : Entity
    {
        public User() 
        {
            IsActive = true;
            FavouriteProducts = new List<FavouriteProduct>();
        }
        //
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public LK_Gender? Gender { get; set; }
        public bool? IsActive { get; set; }
        public string? DeactiveReason { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        //
        public virtual Client? Client { get; set; }
        public virtual Staff? Staff { get; set; }
        public virtual Manager? Manager { get; set; }
        public virtual ICollection<FavouriteProduct> FavouriteProducts { get; set; }
    }
}
