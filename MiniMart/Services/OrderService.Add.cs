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

            var orderParrent = new OrderParrent()
            {
                User = user,
                IsPaid = false,
                DeliveryAddress = "",
                PhoneNumber = user.PhoneNumber ?? "",
                UserName = user.Name,
                LK_OrderStatus = Domain.Enums.LK_OrderStatus.WaitingForPayment,
            };

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
                    OrderParrent = orderParrent,
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
                orderParrent.TotalPrice += order.TotalPrice;
                orderParrent.OriginalPrice += order.OriginalPrice;
                orderParrent.PriceDecreases += order.PriceDecreases;
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
            var orderParrent = await ValidateOrderParrent(request.OrderParrentId);
            
            if(orderParrent.LK_OrderStatus == LK_OrderStatus.WaitingForPayment)
            {
                switch (orderParrent.LK_PaymentMethod)
                {
                    case LK_PaymentMethod.Cash:
                        orderParrent.LK_OrderStatus = LK_OrderStatus.WaitingForDelivery;
                        foreach(var order in orderParrent.Orders)
                        {
                            order.LK_OrderStatus = LK_OrderStatus.WaitingForDelivery;
                            foreach (var item in order.ProductDetails)
                            {
                                var productStore = item.Product.ProductStores.FirstOrDefault(x => x.Store.Id == order.Store.Id);
                                productStore.Quantity = productStore.Quantity - item.Quantity;
                            }
                        }
                        await _unitOfWork.SaveChangeAsync();
                        return new OrderProcessResponse
                        {
                            IsHasError = false,
                            Url = "http://localhost:4200/order"
                        };
                    case LK_PaymentMethod.OnlinePaymnet:
                        var paymentInfoRequest = new PaymentInfoRequest()
                        {
                            OrderParrentId = orderParrent.Id,
                            Amount = orderParrent.TotalPrice.Value
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
