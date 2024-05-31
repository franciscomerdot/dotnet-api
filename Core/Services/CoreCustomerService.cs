using AutoMapper;
using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Services;
using DotNetApi.Core.Data;
using DataModel = DotNetApi.Core.Data.Models;

namespace DotNetApi.Core.Services;

public class CoreCustomerService : CustomerService
{
    private readonly IMapper mapper;
    private readonly DataContext dataContext;


    public CoreCustomerService(IMapper mapper, DataContext dataContext)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Customer> CreateCustomer(CreateCustomerRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required", nameof(request.Name));
        }

        var customer = new DataModel.Customer
        {
            Name = request.Name,
            IsActive = true,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow
        };

        await this.dataContext.Customers.AddAsync(customer);

        this.dataContext.SaveChanges();

        return this.mapper.Map<Customer>(customer);
    }

    public Task<Customer> UpdateCustomer(UpdateCustomerRequest request)
    {
        return Task.FromResult(new Customer());
    }

    public Task<Customer> EnableCustomer(EnableCustomerRequest request)
    {
        return Task.FromResult(new Customer());
    }

    public Task<Customer> DisableCustomer(DisableCustomerRequest request)
    {
        return Task.FromResult(new Customer());
    }

    public Task DeleteCustomer(DeleteCustomerRequest request)
    {
        return Task.CompletedTask;
    }
}

