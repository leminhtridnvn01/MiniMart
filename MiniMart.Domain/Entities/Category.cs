using MiniMart.Domain.Base;

namespace MiniMart.Domain.Entities
{
    public partial class Category : Entity
    {
        public Category()
        {
            Products = new List<Product>();
        }
        //
        public string? Name { get; set; }
        public string? Img { get; set; }
        //
        public virtual ICollection<Product> Products { get; set; }
    }
}
