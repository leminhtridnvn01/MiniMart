using MiniMart.Domain.DTOs.Orders;

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
            var order = await ValidateOrder(request.OrderId);
            order.LK_PaymentMethod = request.LK_PaymentMethod;
            return await _unitOfWork.SaveChangeAsync();
        }
    }
}
