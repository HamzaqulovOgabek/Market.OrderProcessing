using System.ComponentModel.DataAnnotations;

namespace Market.Warehouse.Domain.Models;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
