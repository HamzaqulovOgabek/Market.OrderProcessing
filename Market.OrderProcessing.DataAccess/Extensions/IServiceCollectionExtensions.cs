using E_CommerceProjectDemo.DataAccess.Repositories.OrderItemRepository;
using E_CommerceProjectDemo.DataAccess.Repositories.OrderRepository;
using E_CommerceProjectDemo.DataAccess.Repositories.PaymentRepository;
using Market.OrderProcessing.Application.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_CommerceProjectDemo.DataAccess.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services,
    IConfiguration configuration)
    {
        AddDbContext(services, configuration);

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        
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