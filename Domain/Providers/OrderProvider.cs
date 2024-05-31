using DotNetApi.Domain.DTOs;

namespace DotNetApi.Domain.Providers;

public interface OrderProvider
{
    Task<Order[]> GetCustomerOrders(QueryCustomerOrderRequest request);
    Task<Order> GetCustomerOrder(GetCustomerOrderRequest request);
}

public class QueryCustomerOrderRequest
{
    public virtual int CustomerId { get; set; }
    public virtual string? Status { get; set; }
}

public class GetCustomerOrderRequest
{
    public virtual int CustomerId { get; set; }
    public virtual int Id { get; set; }
}

