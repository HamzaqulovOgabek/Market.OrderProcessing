using Market.OrderProcessing.Domain.Enums;
using Market.OrderProcessing.Domain.Models.Base;
using Market.Warehouse.Domain.Enums;
using Market.Warehouse.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.OrderProcessing.Domain.Models;

[Table(nameof(Order))]
public class Order : Auditable, IHaveStatus, IHaveState
{
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalPrice { get; set; }
    public Status Status { get; set; } = Status.PENDING;
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string Address { get; set; }
    public State State { get; set; } = State.ACTIVE;

    //public User? User { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
}
