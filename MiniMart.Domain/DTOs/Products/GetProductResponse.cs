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
        public IEnumerable<GetProductLocationResponse> Locations { get; set; }

        public virtual Expression<Func<Product, GetProductResponse>> GetSelection()
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
                CategoryId = _.Category != null ? _.Category.Id : 0,
                //Locations = _.ProductStores.Where(s => s.Quantity > 0).Select(s => new ProductStoreResponse()
                //{
                //    CityId = s.Store.Ward.District.City.Id,
                //    CityName = s.Store.Ward.District.City.Name,
                //    StoreId = s.Store.Id,
                //    StoreName = s.Store.Name,
                //    Address = s.Store.Address + ", " + s.Store.Ward.Name + ", " + s.Store.Ward.District.Name + ", " + s.Store.Ward.District.City.Name,
                //    ProductId = s.Product.Id,
                //    ProductName = s.Product.Name,
                //    Quantity = s.Quantity.Value
                //})
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

    //public class ProductStoreResponse
    //{
    //    public ProductStoreResponse()
    //    {

    //    }

    //    public int CityId { get; set; }
    //    public string CityName { get; set; }

    //    public int StoreId { get; set; }
    //    public string StoreName { get; set; }
    //    public string Address { get; set; }
    //    public int ProductId { get; set; }
    //    public string ProductName { get; set; }
    //    public int Quantity { get; set; }

    //    public Expression<Func<ProductStore, ProductStoreResponse>> GetSelection()
    //    {
    //        return _ => new ProductStoreResponse()
    //        {
    //            CityId = _.Store.Ward.District.City.Id,
    //            CityName = _.Store.Ward.District.City.Name,
    //            StoreId = _.Store.Id,
    //            StoreName = _.Store.Name,
    //            Address = _.Store.Address + ", " + _.Store.Ward.Name + ", " + _.Store.Ward.District.Name + ", " + _.Store.Ward.District.City.Name,
    //            ProductId = _.Product.Id,
    //            ProductName = _.Product.Name,
    //            Quantity = _.Quantity.Value
    //        };
    //    }
    //}

}
