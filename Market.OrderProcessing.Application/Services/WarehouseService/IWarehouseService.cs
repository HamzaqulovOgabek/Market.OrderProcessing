using E_CommerceProjectDemo.Application.Services.CartServices;
using Market.OrderProcessing.Application.Services.WarehouseService.Dto;

namespace Market.OrderProcessing.Application.Services.WarehouseService
{
    public interface IWarehouseService
    {
        Task AddToCartAsync(int userId, int productId, int quantity);
        Task ClearCartAsync(int userId);
        Task<CartDetailsDto> GetCartByUserIdAsync(int userId);
        Task<Product> GetProductByIdAsync(int id);
    }
}