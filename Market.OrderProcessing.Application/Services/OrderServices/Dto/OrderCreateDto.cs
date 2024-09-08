namespace Market.OrderProcessing.Application.Services.OrderServices;

public class OrderCreateDto
{
    public int UserId { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Address { get; set; }
}
