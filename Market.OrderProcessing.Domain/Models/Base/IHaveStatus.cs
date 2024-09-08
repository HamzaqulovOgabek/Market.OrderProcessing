using Market.OrderProcessing.Domain.Enums;

namespace Market.OrderProcessing.Domain.Models.Base;

public interface IHaveStatus
{
    Status Status { get; set; }
}
