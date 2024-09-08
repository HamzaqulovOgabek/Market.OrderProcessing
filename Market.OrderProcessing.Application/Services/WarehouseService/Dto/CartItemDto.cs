namespace E_CommerceProjectDemo.Application.Services.CartServices;

public class CartItemDto
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }  
    public int Quantity { get; set; }
}
