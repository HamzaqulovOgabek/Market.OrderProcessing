namespace E_CommerceProjectDemo.Application.Services.CartServices;

public class CartDetailsDto
{
    public int UserId { get; set; }
    public ICollection<CartItemDto> CartItems { get; set; }
    public int TotalDistinctProducts { get; set; }
    public int TotalProductCount { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalPrice { get; set; }

}