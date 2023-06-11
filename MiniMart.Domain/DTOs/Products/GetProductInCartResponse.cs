using MiniMart.Domain.Entities;
using System.Linq.Expressions;

namespace MiniMart.Domain.DTOs.Products
{
    public class GetProductInCartResponse: GetProductResponse
    {
        public GetProductInCartResponse()
        {

        }

        public int Quantity { get; set; }
        public string? StoreName { get; set; }
        public int StoreId { get; set; }

        public Expression<Func<FavouriteProduct, GetProductInCartResponse>> GetSelection()
        {
            return _ => new GetProductInCartResponse()
            {
                Id = _.Product.Id,
                Name = _.Product.Name,
                Img = _.Product.Img,
                Description = _.Product.Description,
                Price = _.Product.Price,
                PriceDecreases = _.Product.PriceDecreases,
                LK_ProductUnit = _.Product.LK_ProductUnit,
                CategoryId = _.Product.Category != null ? _.Product.Category.Id : 0,
                Quantity = _.Quantity,
                StoreName = _.Store.Name ?? "Unknown",
                StoreId = _.Store.Id,
            };
        }

        public GetProductInCartResponse GetMap(Product product, int quantity)
        {
            return new GetProductInCartResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Img = product.Img,
                Description = product.Description,
                Price = product.Price,
                PriceDecreases = product.PriceDecreases,
                LK_ProductUnit = product.LK_ProductUnit,
                CategoryId = product.Category?.Id,
                Quantity = quantity,
            };
        }

    }
}
