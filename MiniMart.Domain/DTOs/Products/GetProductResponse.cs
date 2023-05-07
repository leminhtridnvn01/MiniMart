using MiniMart.Domain.DTOs.Categories;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Enums;
using System.Linq.Expressions;

namespace MiniMart.Domain.DTOs.Products
{
    public class GetProductResponse
    {
        public GetProductResponse()
        {

        }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Img { get; set; }
        public int? Price { get; set; }
        public int? PriceDecreases { get; set; }
        public LK_ProductUnit? LK_ProductUnit { get; set; }
        public int? CategoryId { get; set; }

        public Expression<Func<Product, GetProductResponse>> GetSelection()
        {
            return _ => new GetProductResponse()
            {
                Id = _.Id,
                Name = _.Name,
                Img = _.Img,
                Description = _.Description,
                Price = _.Price,
                PriceDecreases = _.PriceDecreases,
                LK_ProductUnit= _.LK_ProductUnit,
                CategoryId = _.Category != null ? _.Category.Id : 0
            };
        }

        public GetProductResponse GetMap(Product product)
        {
            return new GetProductResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Img = product.Img,
                Description = product.Description,
                Price = product.Price,
                PriceDecreases = product.PriceDecreases,
                LK_ProductUnit = product.LK_ProductUnit,
                CategoryId = product.Category?.Id
            };
        }
    }
}
