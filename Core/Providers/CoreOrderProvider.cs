using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Providers;

namespace DotNetApi.Core.Providers;

public class CoreOrderProvider : OrderProvider
{
    public Task<Order[]> GetCustomerOrders(QueryCustomerOrderRequest request)
    {
        return Task.FromResult(new Order[0]);
    }

    public Task<Order> GetCustomerOrder(GetCustomerOrderRequest request)
    {
        return Task.FromResult(new Order());
    }
}

