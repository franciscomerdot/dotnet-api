using DotNetApi.Domain.DTOs;

namespace DotNetApi.Domain.Providers;

public interface CustomerProvider
{
    Task<Customer[]> GetCustomers(QueryCustomerRequest request);
    Task<Customer> GetCustomer(GetCustomerRequest request);
}

public class QueryCustomerRequest
{
    public virtual string? Name { get; set; }
    public virtual bool? IsActive { get; set; }
}

public class GetCustomerRequest
{
    public virtual int Id { get; set; }
    public virtual bool IncludeContacts { get; set; }
}
