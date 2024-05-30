using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Services;

namespace DotNetApi.Core.Services;

public class CoreCustomerService : CustomerService
{
    public Task<Customer> CreateCustomer(CreateCustomerRequest request)
    {
        return Task.FromResult(new Customer());
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

