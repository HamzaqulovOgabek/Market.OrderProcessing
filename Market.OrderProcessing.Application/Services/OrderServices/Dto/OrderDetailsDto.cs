using Market.OrderProcessing.Application.Services.OrderServices;

namespace E_CommerceProjectDemo.Application.Services.OrderService;

public class OrderDetailsDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? UserName { get; set; } 
    public DateTime OrderDate { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public string? Status { get; set; }    
    public required string Address { get; set; }
    public ICollection<OrderItemDto>? OrderItemsDto { get; set; }
    public decimal TotalPrice { get; set; }

}
