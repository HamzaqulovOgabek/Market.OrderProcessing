using Market.Warehouse.Domain.Models;

namespace Market.OrderProcessing.Domain.Models;

public class Order : BaseEntity<int>
{
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }

}
