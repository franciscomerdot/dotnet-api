using DotNetApi.Domain.DTOs;

namespace DotNetApi.Domain.Providers;

public interface OrderProvider
{
    Task<Order[]> GetCustomerOrders(QueryCustomerOrderRequest request);
    Task<Order> GetCustomerOrder(GetCustomerOrderRequest request);
}

public class QueryCustomerOrderRequest
{
    public int CustomerId { get; set; }
    public string status { get; set; } = default!;
}

public class GetCustomerOrderRequest
{
    public int CustomerId { get; set; }
    public int Id { get; set; }
}

