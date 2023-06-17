using MiniMart.Domain.Entities;
using System.Linq.Expressions;

namespace MiniMart.Domain.DTOs.Products
{
    public class GetSaleProductResponse : GetProductResponse
    {
        public GetSaleProductResponse()
        {

        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }

        public virtual Expression<Func<ProductStore, GetSaleProductResponse>> GetSelection()
        {
            return _ => new GetSaleProductResponse()
            {
                Id = _.Product.Id,
                Name = _.Product.Name,
                Img = _.Product.Img,
                Description = _.Product.Description,
                Price = _.Product.Price,
                PriceDecreases = _.PriceDecreases.HasValue && _.PriceDecreases.Value > 0
                                 ? _.PriceDecreases.Value
                                 : _.Product.PriceDecreases,
                LK_ProductUnit = _.Product.LK_ProductUnit,
                CategoryId = _.Product.Category != null ? _.Product.Category.Id : 0,
                StoreId = _.Store.Id,
                StoreName = _.Store.Name ?? "Unknow"
            };
        }
    }
}
