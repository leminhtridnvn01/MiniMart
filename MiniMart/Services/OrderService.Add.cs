using Microsoft.EntityFrameworkCore;
using MiniMart.API.Extensions;
using MiniMart.Domain.DTOs.Orders;
using MiniMart.Domain.DTOs.Products;
using MiniMart.Domain.Entities;
using System.Linq;

namespace MiniMart.API.Services
{
    public partial class OrderService
    {
        public async Task<bool> AddOrder(AddOrderRequest request)
        {
            var user = await ValidateUser(_user.GetUserId());
            var store = await ValidateStore(request.StoreId);
            var productIds = request.Products.Select(x => x.ProductId).ToList();

            //Create new Order
            var order = new Order()
            {
                User = user,
                Store = store,
                IsPaid = false,
            };

            // Create list ProductDetail
            var productDetails = new List<ProductDetail>();
            foreach (var item in request.Products)
            {
                var product = _productRepository.GetQuery(x => x.Id == item.ProductId).FirstOrDefault();
                if (product != null)
                {
                    var productDetail = new ProductDetail(product, order, item.Quantity);
                    productDetails.Add(productDetail);
                    order.OriginalPrice += productDetail.OriginalPrice;
                    order.PriceDecreases += productDetail.PriceDecreases;
                    order.TotalPrice += productDetail.TotalPrice;
                }
            }
            _productDetailRepository.InsertRange(productDetails);

            // Remove Product out of Cart
            var favouriteProducts = _favouriteProductRepository.GetQuery(x => productIds.Contains(x.Product.Id) && x.User.Id == user.Id);
            _favouriteProductRepository.RemoveRange(favouriteProducts);

            // Save
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
