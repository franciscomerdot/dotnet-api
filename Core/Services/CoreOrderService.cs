using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Services;

namespace DotNetApi.Core.Services;

public class CoreOrderService : OrderService
{
    public Task<Order> CreateOrder(CreateOrderRequest request)
    {
        return Task.FromResult(new Order());
    }

    public Task<Order> CancelCustomerOrder(CancelCustomerOrderRequest request)
    {
        return Task.FromResult(new Order());
    }
}

