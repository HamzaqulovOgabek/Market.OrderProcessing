using Market.OrderProcessing.Domain.Enums;
using Market.OrderProcessing.Domain.Models.Base;
using Market.Warehouse.Domain.Enums;
using Market.Warehouse.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.OrderProcessing.Domain.Models;

[Table(nameof(Payment))]
public class Payment : Auditable, IHaveStatus, IHaveState
{
    public int UserId { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CREDITCARD;
    public DateTime PaymentDate { get; set; } = DateTime.Now;
    public Status Status { get; set; } = Status.PENDING;
    public State State { get; set; }
    //public User? User { get; set; }
    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }
}
