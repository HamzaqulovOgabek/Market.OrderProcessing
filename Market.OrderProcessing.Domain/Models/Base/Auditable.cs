namespace Market.Warehouse.Domain.Models;

public abstract class Auditable : BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; } = DateTime.Now;
    public int CreatedUserId { get; set; }
    public int ModifiedUserId { get; set; }
}
