using E_CommerceProjectDemo.Application.Services.PaymentServices;

namespace Market.OrderProcessing.Application.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<PaymentDto> GetAsync(int id);
        IQueryable<PaymentDto> GetUserPayments(int userId);
        Task<bool> IsPayedForOrderAsync(int orderId);
        Task<int> ProcessPaymentAsync(PaymentCreateDto dto);
    }
}