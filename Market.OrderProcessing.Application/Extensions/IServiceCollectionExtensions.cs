using E_CommerceProjectDemo.Application.Services.PaymentServices;
using E_CommerceProjectDemo.DataAccess.Repositories.OrderItemRepository;
using Market.Auth.Application.Services.UserServices;
using Market.OrderProcessing.Application.Context;
using Market.OrderProcessing.Application.Services.OrderServices;
using Market.OrderProcessing.Application.Services.OrderServices.Implements;
using Market.OrderProcessing.Application.Services.PaymentServices;
using Market.OrderProcessing.Application.Services.WarehouseService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.OrderProcessing.Application.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services,
    IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        services.AddHttpClient();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<IUserService, UserService>();


        return services;
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderProcessingDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Market.OrderProcessing"));
        });
    }
}
