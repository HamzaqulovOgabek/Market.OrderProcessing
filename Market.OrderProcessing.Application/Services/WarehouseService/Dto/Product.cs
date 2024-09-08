using Market.Warehouse.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Market.OrderProcessing.Application.Services.WarehouseService.Dto
{
    public class Product
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Color { get; set; }
        public string? Features { get; set; }
        public string? Material { get; set; }
        public int Quantity { get; set; }
        public int BrandId { get; set; }
        public int? DiscountId { get; set; }
        public int? CategoryId { get; set; }
        public int WareHouseId { get; set; } = 1;
        public State State { get; set; } = State.ACTIVE;
    }
}
