using System.ComponentModel.DataAnnotations;

namespace Market.Warehouse.Domain.Models;

public abstract class BaseEntity<TId> where TId : struct
{
    [Key]
    public TId Id { get; set; }
}
