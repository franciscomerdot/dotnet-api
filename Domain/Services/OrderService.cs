using DotNetApi.Domain.DTOs;

namespace DotNetApi.Domain.Services;

public interface OrderService
{
    Task<Order> CreateOrder(CreateOrderRequest request);
    Task<Order> CancelOrder(CancelOrderRequest request);
}

public class CreateOrderRequest
{
    public int CustomerId { get; set; }
    public decimal ServiceQty { get; set; }
}

public class CancelOrderRequest
{
    public int Id { get; set; }
}
