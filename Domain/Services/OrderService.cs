using DotNetApi.Domain.DTOs;

namespace DotNetApi.Domain.Services;

public interface OrderService
{
    Task<Order> CreateOrder(CreateOrderRequest request);
    Task<Order> CancelCustomerOrder(CancelCustomerOrderRequest request);
}

public class CreateOrderRequest
{
    public virtual int CustomerId { get; set; }
    public virtual decimal ServiceQty { get; set; }
}

public class CancelCustomerOrderRequest
{
    public virtual int CustomerId { get; set; }
    public virtual int Id { get; set; }
}
