using E_CommerceProjectDemo.DataAccess.Repositories.Base;
using Market.OrderProcessing.Application.Context;
using Market.OrderProcessing.Domain.Models;

namespace E_CommerceProjectDemo.DataAccess.Repositories.PaymentRepository;

public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(OrderProcessingDbContext context) : base(context)
    {
    }
}
