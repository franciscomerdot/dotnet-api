using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Providers;
using DotNetApi.Core.Data;

namespace DotNetApi.Core.Providers;

public class CoreOrderProvider : OrderProvider
{
    private readonly IMapper mapper;
    private readonly DataContext dataContext;

    public CoreOrderProvider(IMapper mapper, DataContext dataContext)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Order[]> GetCustomerOrders(QueryCustomerOrderRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request.CustomerId <= 0)
        {
            throw new ArgumentException("CustomerId is required", nameof(request.CustomerId));
        }

        var query = this.dataContext.Orders
            .Where(x => x.CustomerId == request.CustomerId)
            .OrderBy(x => x.Id)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            query = query.Where(x => x.Status == request.Status);
        }

        return this.mapper.Map<Order[]>(await query.ToArrayAsync());
    }

    public async Task<Order> GetCustomerOrder(GetCustomerOrderRequest request)
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
            throw new ArgumentException("Id is required", nameof(request.Id));
        }

        var query = this.dataContext.Orders
            .Where(x => x.CustomerId == request.CustomerId && x.Id == request.Id)
            .AsQueryable();

        var order = this.mapper.Map<Order>(await query.FirstOrDefaultAsync());

        if (order == null)
        {
            throw new ArgumentException("Order not found", nameof(request.Id));
        }

        return order;
    }
}

