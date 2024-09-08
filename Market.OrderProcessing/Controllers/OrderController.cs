using Market.OrderProcessing.Application.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;

namespace Market.OrderProcessing.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderByIdAsync(int id)
    {
        return Ok(await _service.GetOrderByIdAsync(id));
    }
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserOrdersAsync(int userId)
    {
        return Ok(await _service.GetUserOrdersAsync(userId));
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(OrderCreateDto dto)
    {
        return Ok(await _service.CreateOrderAsync(dto));
    }
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> CancelOrderAsync(int orderId)
    {
        await _service.CalcelOrder(orderId);
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> AddProductToOrderAsync(int orderId, int productId, int quantity)
    {
        return Ok(await _service.AddProductToOrder(orderId, productId, quantity));
    }
    [HttpPost]
    public async Task<IActionResult> RemoveProductFromOrderAsync(int orderId, int productId)
    {
        await _service.RemoveProductFromOrderAsync(orderId, productId);
        return Ok();
    }

}
