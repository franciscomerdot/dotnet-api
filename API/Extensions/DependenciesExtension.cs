using AutoMapper;
using DotNetApi.Domain.Providers;
using DotNetApi.Domain.Services;
using DotNetApi.Core.Providers;
using DotNetApi.Core.Services;
using Data = DotNetApi.Core.Data;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependenciesExtension
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

        // Oher
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Data.Mappers.CustomerMapper>();
            cfg.AddProfile<Data.Mappers.CustomerContactMapper>();
            cfg.AddProfile<Data.Mappers.OrderMapper>();
        });
        services.AddSingleton<IMapper>(mapperConfig.CreateMapper());
        services.AddScoped<Data.DataContext>();

        return services;
    }
}
