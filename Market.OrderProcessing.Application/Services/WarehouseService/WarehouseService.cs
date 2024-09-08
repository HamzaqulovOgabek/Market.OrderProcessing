using E_CommerceProjectDemo.Application.Services.CartServices;
using Market.OrderProcessing.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using static System.Net.WebRequestMethods;
using System.Text.Json;
using Market.OrderProcessing.Application.Services.WarehouseService.Dto;

namespace Market.OrderProcessing.Application.Services.WarehouseService;

public class WarehouseService : IWarehouseService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly string _authServiceUrl;
    private readonly string _warehouseServiceUrl;
    public WarehouseService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _authServiceUrl = configuration["Services:AuthService"];
        _warehouseServiceUrl = configuration["Services:WarehouseService"];
    }

    public async Task<CartDetailsDto> GetCartByUserIdAsync(int userId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var cartResponse = await httpClient.GetAsync($"{_warehouseServiceUrl}/api/Cart/GetCart/{userId}");
        if (!cartResponse.IsSuccessStatusCode)
        {
            throw new EntityNotFoundException("User not found");
        }
        var cartDetailsDto = await cartResponse.Content.ReadFromJsonAsync<CartDetailsDto>();
        if (cartDetailsDto == null)
        {
            throw new EntityNotFoundException("User not found");
        }
        return cartDetailsDto;
    }
    public async Task ClearCartAsync(int userId)
    {
        // Find all cart items for the user
        var httpClient = _httpClientFactory.CreateClient();
        var cartResponse = await httpClient.DeleteAsync($"{_warehouseServiceUrl}/api/Cart/ClearCart/{userId}/clear");
        if (!cartResponse.IsSuccessStatusCode)
        {
            throw new EntityNotFoundException("User not found");
        }
    }
    public async Task AddToCartAsync(int userId, int productId, int quantity)
    {
        var httpClient = _httpClientFactory.CreateClient();

        // Construct the request URL
        var requestUrl = $"{_warehouseServiceUrl}/api/Cart/AddToCart/{userId}/add";

        // Create the request body
        var requestBody = new
        {
            ProductId = productId,
            Quantity = quantity
        };

        // Serialize the request body to JSON
        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        // Send the POST request
        var cartResponse = await httpClient.PostAsync(requestUrl, content);

        // Check if the response was successful
        if (!cartResponse.IsSuccessStatusCode)
        {
            throw new Exception("Failed to add item to the cart.");
        }
    }
    public async Task<Product> GetProductByIdAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient();

        var requestUrl = $"{_warehouseServiceUrl}/api/Product/{id}";
        var response = await httpClient.GetAsync(requestUrl);
        if (!response.IsSuccessStatusCode)
        {
            throw new EntityNotFoundException("Product not found");
        }
        var product = await response.Content.ReadFromJsonAsync<Product>();
        if (product == null)
        {
            throw new EntityNotFoundException("Product not found");
        }

        return product;
    }

}
