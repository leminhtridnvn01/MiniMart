using MiniMart.Domain.Entities;
using MiniMart.Domain.Enums;
using System.Linq.Expressions;

namespace MiniMart.Domain.DTOs.Products
{
    public class AddProductRequest
    {

        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public int? PriceDecreases { get; set; }
        public LK_ProductUnit LK_ProductUnit { get; set; }

        public Expression<Func<AddProductRequest, Product>> GetSelection()
        {
            return _ => new Product() 
            { 
                Name = _.Name, 
                Price = _.Price, 
                PriceDecreases = _.PriceDecreases, 
                LK_ProductUnit = _.LK_ProductUnit, 
                UpdateOn = DateTime.UtcNow, 
                CreateOn = DateTime.UtcNow 
            };
        }

        public Product GetMap()
        {
            return new Product()
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                PriceDecreases = this.PriceDecreases,
                LK_ProductUnit = this.LK_ProductUnit,
                UpdateOn = DateTime.UtcNow,
                CreateOn = DateTime.UtcNow
            };
        }
    }
}
