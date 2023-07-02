using MediatR;
using MiniMart.API.Extensions;
using MiniMart.Domain.DTOs.Orders;
using MiniMart.Domain.Entities;

namespace MiniMart.API.Services
{
    public partial class OrderService
    {
        public async Task<bool> UpdateDeliveryInfoOrderAsync(UpdateDeliveryAddressOrderRequest request)
        {
            var order = await ValidateOrder(request.OrderId);
            order.UpdateDeliveryInfo(request.UserName, request.DeliveryAddress, request.ContactPhoneNumber);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateOrderTypeAsync(UpdateOrderTypeRequest request)
        {
            var order = await ValidateOrder(request.OrderId);
            order.LK_OrderType = request.LK_OrderType;
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdatePaymentMethodAsync(UpdatePaymentMethodRequest request)
        {
            var orderParrent = await ValidateOrderParrent(request.OrderParrentId);
            orderParrent.LK_PaymentMethod = request.LK_PaymentMethod;
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdatePickupTimeAsync(UpdatePickupTimeRequest request)
        {
            var order = await ValidateOrder(request.OrderId);
            order.PickupTimeFrom = request.PickupTime;
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> ApproveOrderAsync(int orderId)
        {
            var order = await ValidateOrder(orderId);
            order.IsApproved = true;
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusRequest request)
        {
            var order = await ValidateOrder(request.OrderId);
            switch (request.OrderStatus)
            {
                case Domain.Enums.LK_OrderStatus.None:
                    break;
                case Domain.Enums.LK_OrderStatus.WaitingForPayment:
                    var user = await ValidateUser(_user.GetUserId());
                    var orderParrent = new OrderParrent()
                    {
                        User = user,
                        IsPaid = false,
                        DeliveryAddress = "",
                        PhoneNumber = user.PhoneNumber ?? "",
                        UserName = user.Name,
                        TotalPrice = order.TotalPrice,
                        LK_OrderStatus = Domain.Enums.LK_OrderStatus.WaitingForPayment,
                    };
                    order.OrderParrent = orderParrent;
                    order.LK_OrderStatus = request.OrderStatus;
                    break;
                case Domain.Enums.LK_OrderStatus.WaitingForDelivery:
                    order.LK_OrderStatus = request.OrderStatus;
                    break;
                case Domain.Enums.LK_OrderStatus.DeliveryCancle:
                    order.LK_OrderStatus = request.OrderStatus;
                    break;
                case Domain.Enums.LK_OrderStatus.Complete:
                    order.LK_OrderStatus = request.OrderStatus;
                    break;
                case Domain.Enums.LK_OrderStatus.RejectForPayment:
                    order.LK_OrderStatus = request.OrderStatus;
                    break;
                default:
                    break;
            }
            
            return await _unitOfWork.SaveChangeAsync();
        }
    }
}
