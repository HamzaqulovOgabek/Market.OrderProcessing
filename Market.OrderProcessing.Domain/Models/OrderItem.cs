using Market.OrderProcessing.Domain.Enums;
using Market.OrderProcessing.Domain.Models.Base;
using Market.Warehouse.Domain.Enums;
using Market.Warehouse.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.OrderProcessing.Domain.Models;

[Table(nameof(OrderItem))]
public class OrderItem : Auditable, IHaveStatus, IHaveState
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Status Status { get; set; } = Status.PROCESSING;
    public State State { get; set; }
    //public Product? Product { get; set; }
    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }
}
