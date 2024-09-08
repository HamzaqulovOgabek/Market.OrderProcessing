using E_CommerceProjectDemo.DataAccess.Repositories.Base;
using Market.OrderProcessing.Application.Context;
using Market.OrderProcessing.Domain.Models;

namespace E_CommerceProjectDemo.DataAccess.Repositories.OrderItemRepository;

public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(OrderProcessingDbContext context) : base(context)
    {
    }

    public async Task CreateOrderItemsAsync(List<OrderItem> orderItems)
    {
        await Context.OrderItems.AddRangeAsync(orderItems);
    }
}
