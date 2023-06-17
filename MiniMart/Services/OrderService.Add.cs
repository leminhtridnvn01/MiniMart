using Microsoft.EntityFrameworkCore;
using MiniMart.API.Exceptions;
using MiniMart.API.Extensions;
using MiniMart.Domain.DTOs.Orders;
using MiniMart.Domain.DTOs.Payments;
using MiniMart.Domain.DTOs.Products;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Enums;
using System.Linq;
using System.Net;

namespace MiniMart.API.Services
{
    public partial class OrderService
    {
        public async Task<bool> AddOrder(AddOrderRequest request)
        {
            var user = await ValidateUser(_user.GetUserId());
            //var store = await ValidateStore(request.StoreId);
            var productIds = request.Products.Select(x => x.ProductId).ToList();

            

            // Create list ProductDetail
            var productDetails = new List<ProductDetail>();
            var productGroups = request.Products.GroupBy(x => x.StoreId).ToList();
            foreach (var productGroup in productGroups)
            {
                //Create new Order
                var store = await ValidateStore(productGroup.Key);
                var order = new Order()
                {
                    User = user,
                    Store = store,
                    IsPaid = false,
                    LK_OrderStatus = Domain.Enums.LK_OrderStatus.WaitingForPayment,
                    DeliveryAddress = "",
                    PhoneNumber = user.PhoneNumber ?? "",
                    UserName = user.Name,
                };

                foreach (var item in productGroup)
                {
                    var product = await ValidateProduct(item.ProductId, store.Id);

                    var productDetail = new ProductDetail(product, order, item.Quantity);
                    productDetails.Add(productDetail);
                    order.OriginalPrice += (productDetail.OriginalPrice * productDetail.Quantity);
                    order.PriceDecreases += (productDetail.PriceDecreases * productDetail.Quantity);
                    order.TotalPrice += productDetail.TotalPrice;
                }
            }
            //foreach (var item in request.Products)
            //{
            //    var product = await ValidateProduct(item.ProductId);

            //    var productDetail = new ProductDetail(product, order, item.Quantity);
            //    productDetails.Add(productDetail);
            //    order.OriginalPrice += productDetail.OriginalPrice;
            //    order.PriceDecreases += productDetail.PriceDecreases;
            //    order.TotalPrice += productDetail.TotalPrice;
            //}
            _productDetailRepository.InsertRange(productDetails);

            // Remove Product out of Cart
            var favouriteProducts = _favouriteProductRepository.GetQuery(x => productIds.Contains(x.Product.Id) && x.User.Id == user.Id);
            _favouriteProductRepository.RemoveRange(favouriteProducts);

            // Save
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<OrderProcessResponse> ProcessOrderAsync(OrderInfo request)
        {
            var order = await ValidateOrder(request.OrderId);
            
            if(order.LK_OrderStatus == LK_OrderStatus.WaitingForPayment)
            {
                switch (order.LK_PaymentMethod)
                {
                    case LK_PaymentMethod.Cash:
                        order.LK_OrderStatus = LK_OrderStatus.WaitingForDelivery;
                        await _unitOfWork.SaveChangeAsync();
                        return new OrderProcessResponse
                        {
                            IsHasError = false,
                            Url = "http://localhost:4200/order"
                        };
                    case LK_PaymentMethod.OnlinePaymnet:
                        var paymentInfoRequest = new PaymentInfoRequest()
                        {
                            OrderId = order.Id,
                            Amount = order.TotalPrice.Value
                        };
                        var url = await _paymentService.Pay(paymentInfoRequest);
                        return new OrderProcessResponse
                        {
                            IsHasError = false,
                            Url = url
                        };
                    default:
                        throw new HttpException(HttpStatusCode.BadRequest, "An error has occurred!");
                }
            }

            throw new HttpException(HttpStatusCode.BadRequest, "This Order can not pay again!");
        }
    }
}
