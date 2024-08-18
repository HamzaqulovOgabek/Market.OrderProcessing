using Market.Warehouse.Domain.Enums;

namespace Market.Warehouse.Domain.Models
{
    public interface IHaveState
    {
        public State State { get; set; }
    }
}
