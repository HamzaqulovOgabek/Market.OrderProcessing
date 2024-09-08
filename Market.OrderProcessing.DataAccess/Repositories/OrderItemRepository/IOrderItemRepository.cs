using E_CommerceProjectDemo.DataAccess.Repositories.Base;
using Market.OrderProcessing.Domain.Models;

namespace E_CommerceProjectDemo.DataAccess.Repositories.OrderItemRepository;

public interface IOrderItemRepository : IBaseRepository<OrderItem>
{
    Task CreateOrderItemsAsync(List<OrderItem> orderItems);
}
