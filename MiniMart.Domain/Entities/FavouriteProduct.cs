using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public class FavouriteProduct : Entity
    {
        public FavouriteProduct()
        {

        }
        //

        //
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
