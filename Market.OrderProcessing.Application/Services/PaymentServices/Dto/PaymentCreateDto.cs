namespace E_CommerceProjectDemo.Application.Services.PaymentServices;

public class PaymentCreateDto
{
    public int UserId { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
}
