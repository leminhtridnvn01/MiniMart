using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public partial class Store : Entity
    {
        public Store()
        {
            Staffs = new List<Staff>();
            ProductStores = new List<ProductStore>();
            FavouriteProducts = new List<FavouriteProduct>();
        }
        //
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Address { get; set; }
        //
        public virtual ICollection<Staff> Staffs { get; set;}
        public virtual Ward? Ward { get; set; }
        public virtual ICollection<ProductStore> ProductStores { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<FavouriteProduct> FavouriteProducts { get; set; }
    }
}
