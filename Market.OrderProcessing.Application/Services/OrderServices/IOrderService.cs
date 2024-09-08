using E_CommerceProjectDemo.Application.Services.OrderService;
using Market.OrderProcessing.Application.Services.Base;
using Market.OrderProcessing.Domain.Models;

namespace Market.OrderProcessing.Application.Services.OrderServices;

public interface IOrderService
{
    Task<int> AddProductToOrder(int orderId, int productId, int quantity);
    Task CalcelOrder(int orderId);
    Task<int> CreateOrderAsync(OrderCreateDto dto);
    IQueryable<Order> GetAll();
    Task<OrderDetailsDto> GetOrderByIdAsync(int id);
    Task<IQueryable<OrderDetailsDto>> GetUserOrdersAsync(int userId);
    Task RemoveProductFromOrderAsync(int orderId, int productId);
}