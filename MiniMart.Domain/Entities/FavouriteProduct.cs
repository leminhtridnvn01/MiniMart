using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public partial class FavouriteProduct : Entity
    {
        public FavouriteProduct()
        {

        }
        //
        public int Quantity { get; set; }
        //
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
