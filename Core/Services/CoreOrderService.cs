using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Services;
using DotNetApi.Core.Data;
using DataModel = DotNetApi.Core.Data.Models;

namespace DotNetApi.Core.Services;

public class CoreOrderService : OrderService
{
    private readonly IMapper mapper;
    private readonly DataContext dataContext;

    public CoreOrderService(IMapper mapper, DataContext dataContext)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Order> CreateOrder(CreateOrderRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request.CustomerId <= 0)
        {
            throw new ArgumentException("CustomerId is required", nameof(request.CustomerId));
        }

        if (request.ServiceQty <= 0)
        {
            throw new ArgumentException("ServiceQty is required", nameof(request.ServiceQty));
        }

        var customer = await this.dataContext.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId);

        if (customer == null)
        {
            throw new ArgumentException("Customer not found", nameof(request.CustomerId));
        }

        var order = new DataModel.Order
        {
            CustomerId = customer.Id,
            Customer = customer,
            Status = "pending",
            OrderNumber = String.Empty,
            Total = request.ServiceQty * 100,
            CreatedAt = DateTime.UtcNow
        };

        this.dataContext.Orders.Add(order);

        this.dataContext.SaveChanges();

        order.OrderNumber = $"ORD{order.Id.ToString().PadLeft(6, '0')}";

        this.dataContext.SaveChanges();

        return this.mapper.Map<Order>(order);
    }

    public async Task<Order> CancelCustomerOrder(CancelCustomerOrderRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request.CustomerId <= 0)
        {
            throw new ArgumentException("CustomerId is required", nameof(request.CustomerId));
        }

        if (request.Id <= 0)
        {
            throw new ArgumentException("OrderId is required", nameof(request.Id));
        }

        var order = await this.dataContext.Orders.FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId && x.Id == request.Id);

        if (order == null)
        {
            throw new ArgumentException("Order not found", nameof(request.Id));
        }

        if (order.Status == "cancelled")
        {
            throw new ArgumentException("Order already cancelled", nameof(request.Id));
        }

        // TODO: Check orher status that prevent cancellation

        order.Status = "cancelled";

        this.dataContext.SaveChanges();

        return this.mapper.Map<Order>(order);
    }
}

