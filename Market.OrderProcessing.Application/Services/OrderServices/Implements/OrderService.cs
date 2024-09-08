using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_CommerceProjectDemo.Application.Services.OrderService;
using E_CommerceProjectDemo.DataAccess.Repositories.OrderItemRepository;
using E_CommerceProjectDemo.DataAccess.Repositories.OrderRepository;
using Market.Auth.Application.Services.UserServices;
using Market.OrderProcessing.Application.Exceptions;
using Market.OrderProcessing.Application.Services.PaymentServices;
using Market.OrderProcessing.Application.Services.WarehouseService;
using Market.OrderProcessing.Domain.Enums;
using Market.OrderProcessing.Domain.Models;
using Market.Warehouse.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Market.OrderProcessing.Application.Services.OrderServices.Implements;

public class OrderService : IOrderService
{
    private readonly IHttpClientFactory? _httpClientFactory;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IUserService _userService;
    private readonly IPaymentService _paymentService;
    private readonly IWarehouseService _warehouseService;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        IUserService userService,
        IWarehouseService warehouseService,
        IPaymentService paymentService,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _userService = userService;
        _paymentService = paymentService;
        _warehouseService = warehouseService;
        _mapper = mapper;
    }

    public async Task<int> CreateOrderAsync(OrderCreateDto dto)
    {
        // Step 1: Check if the user exists by communicating with the Auth microservice
        var user = await _userService.GetAsync(dto.UserId);
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }

        // Step 2: Get the user's cart from the Warehouse microservice
        var cart = await _warehouseService.GetCartByUserIdAsync(dto.UserId);
        if (!cart.CartItems.Any())
        {
            throw new InvalidOperationException("You cannot place an order. Explore Products");
        }
        // Step 3: Create a new order
        var order = new Order
        {
            UserId = dto.UserId,
            City = dto.City,
            PostalCode = dto.PostalCode,
            Address = dto.Address,
            TotalPrice = cart.TotalPrice,
            Status = Status.CREATED
        };

        // Step 4: Save the order and get the order ID
        var orderId = await _orderRepository.CreateAsync(order);

        // Step 5: Create order items from the cart items
        var orderItems = cart.CartItems.Select(ci => new OrderItem
        {
            ProductId = ci.ProductId,
            Quantity = ci.Quantity,
            Price = ci.Price,
            OrderId = orderId
        }).ToList();
        await _orderItemRepository.CreateOrderItemsAsync(orderItems);

        // Step 7: Clear the user's cart in the Warehouse microservice
        await _warehouseService.ClearCartAsync(dto.UserId);

        return order.Id;
    }
    public async Task CalcelOrder(int orderId)
    {
        var isPayed = await _paymentService.IsPayedForOrderAsync(orderId);
        if (isPayed)
            throw new InvalidOperationException("You cannot cancel order");

        var order = await _orderRepository.GetByIdAsync(orderId);
        foreach (var orderItem in order.OrderItems)
        {
            await _warehouseService.AddToCartAsync(order.UserId, orderItem.ProductId, orderItem.Quantity);
            orderItem.Status = Status.CANCELED;
            await _orderItemRepository.UpdateAsync(orderItem);
        }

        order.Status = Status.CANCELED;
        await _orderRepository.UpdateAsync(order);
    }
    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
    public IQueryable<Order> GetAll()
    {
        return _orderRepository.GetAll();
    }
    public async Task<OrderDetailsDto> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            throw new EntityNotFoundException("Order not found");
        return _mapper.Map<OrderDetailsDto>(order);
    }
    public async Task<IQueryable<OrderDetailsDto>> GetUserOrdersAsync(int userId)
    {
        var user = await _userService.GetAsync(userId);
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }

        var ordersQuery = _orderRepository.GetAll()
            .Include(o => o.OrderItems)
            .Where(o => o.UserId == userId
                && o.Status != Status.CANCELED
                && o.State == State.ACTIVE)
            .ProjectTo<OrderDetailsDto>(_mapper.ConfigurationProvider);
        if (ordersQuery == null || !await ordersQuery.AnyAsync())
        {
            throw new InvalidOperationException("Order not found for this user");
        }

        return ordersQuery;
    }
    public async Task<int> AddProductToOrder(int orderId, int productId, int quantity)
    {
        var product = await _warehouseService.GetProductByIdAsync(productId);
        //add order to orderItem
        var orderItem = new OrderItem
        {
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            Price = product.Price,
        };
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new EntityNotFoundException("Order not found");

        order.TotalPrice += orderItem.Price * orderItem.Quantity;
        order.Status = Status.UPDATED;

        var entity = _mapper.Map<Order>(order);
        await _orderRepository.UpdateAsync(entity);
        return await _orderItemRepository.CreateAsync(orderItem);
    }
    public async Task RemoveProductFromOrderAsync(int orderId, int productId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        var orderItem = order.OrderItems.FirstOrDefault(o => o.OrderId == orderId
                                                          && o.ProductId == productId);
        if (orderItem == null)
            throw new EntityNotFoundException("order item not found");

        order.Status = Status.UPDATED;
        order.TotalPrice -= orderItem.Price * orderItem.Quantity;

        await _orderItemRepository.DeleteAsync(orderItem.Id);
    }
    
}
