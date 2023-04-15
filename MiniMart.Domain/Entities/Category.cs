using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public class Category : Entity
    {
        public Category()
        {
            Products = new List<Product>();
        }
        //
        public string? Name { get; set; }
        //
        public virtual ICollection<Product> Products { get; set; }
    }
}
