using DotNetApi.Domain.DTOs;

namespace DotNetApi.Domain.Providers;

public interface CustomerProvider
{
    Task<Customer[]> GetCustomers(QueryCustomerRequest request);
    Task<Customer> GetCustomer(GetCustomerRequest request);
}

public class QueryCustomerRequest
{
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
}

public class GetCustomerRequest
{
    public int Id { get; set; }
    public bool IncludeContacts { get; set; }
}
