using DotNetApi.Domain.Providers;
using DotNetApi.Domain.Services;
using DotNetApi.Core.Providers;
using DotNetApi.Core.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class CoreDependenciesExtension
{
    public static IServiceCollection AddCoreDependencies(
        this IServiceCollection services)
    {
        // Providers
        services.AddScoped<CustomerProvider, CoreCustomerProvider>();
        services.AddScoped<CustomerContactProvider, CoreCustomerContactProvider>();
        services.AddScoped<OrderProvider, CoreOrderProvider>();

        // Services
        services.AddScoped<CustomerService, CoreCustomerService>();
        services.AddScoped<CustomerContactService, CoreCustomerContactService>();
        services.AddScoped<OrderService, CoreOrderService>();


        return services;
    }
}
