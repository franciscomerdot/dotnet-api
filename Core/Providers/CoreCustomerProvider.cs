using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Providers;

namespace DotNetApi.Core.Providers;

public class CoreCustomerProvider : CustomerProvider
{
    public Task<Customer[]> GetCustomers(QueryCustomerRequest request)
    {
        return Task.FromResult(new Customer[0]);
    }

    public Task<Customer> GetCustomer(GetCustomerRequest request)
    {
        return Task.FromResult(new Customer());
    }
}

