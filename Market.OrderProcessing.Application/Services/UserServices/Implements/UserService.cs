using Market.OrderProcessing.Application.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Market.Auth.Application.Services.UserServices;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly string _authServiceUrl;
    private readonly string _warehouseServiceUrl;
    public UserService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _authServiceUrl = configuration["Services:AuthService"];
        _warehouseServiceUrl = configuration["Services:WarehouseService"];
    }
    public async Task<UserBaseDto> GetAsync(int userId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var userResponse = await httpClient.GetAsync($"{_authServiceUrl}/api/user/Get/{userId}");
        if (!userResponse.IsSuccessStatusCode)
        {
            throw new EntityNotFoundException("User not found");
        }

        var user = await userResponse.Content.ReadFromJsonAsync<UserBaseDto>();
        if (user == null)
        {
            return null;
        }
        return user;
    }

}
