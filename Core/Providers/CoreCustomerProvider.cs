using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Providers;
using DotNetApi.Core.Data;

namespace DotNetApi.Core.Providers;

public class CoreCustomerProvider : CustomerProvider
{
    private readonly IMapper mapper;
    private readonly DataContext dataContext;

    public CoreCustomerProvider(IMapper mapper, DataContext dataContext)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Customer[]> QueryCustomers(QueryCustomerRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var query = this.dataContext.Customers
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Id)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(x => x.IsActive == request.IsActive.Value);
        }

        return mapper.Map<Customer[]>(await query.ToArrayAsync());
    }

    public async Task<Customer> GetCustomer(GetCustomerRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request.Id <= 0)
        {
            throw new ArgumentException("Id is required", nameof(request.Id));
        }

        var query = this.dataContext.Customers
            .Where(x => x.Id == request.Id)
            .AsQueryable();

        if (request.IncludeContacts)
        {
            query = query.Include(x => x.Contacts);
        }

        var customer = await query.FirstOrDefaultAsync();

        if (customer == null)
        {
            throw new ArgumentException("Customer not found", nameof(request.Id));
        }

        return mapper.Map<Customer>(customer);
    }
}

