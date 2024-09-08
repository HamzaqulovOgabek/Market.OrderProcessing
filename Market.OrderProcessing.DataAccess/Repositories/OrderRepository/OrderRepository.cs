using E_CommerceProjectDemo.DataAccess.Repositories.Base;
using Market.OrderProcessing.Application.Context;
using Market.OrderProcessing.Domain.Models;
using Market.Warehouse.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProjectDemo.DataAccess.Repositories.OrderRepository;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(OrderProcessingDbContext context) : base(context)
    {
    }
    public override async Task<Order?> GetByIdAsync(int id)
    {
        var order = await Context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id && o.State == State.ACTIVE);
        return order;
    }
}
