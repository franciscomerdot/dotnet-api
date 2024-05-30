using DotNetApi.Domain.DTOs;

namespace DotNetApi.Domain.Providers;

public interface CustomerContactProvider
{
    Task<Contact[]> GetCustomerContacts(QueryCustomerContactRequest request);
    Task<Contact> GetCustomerContact(GetCustomerContactRequest request);
}

public class QueryCustomerContactRequest
{
    public virtual int CustomerId { get; set; }
    public virtual string? Name { get; set; }
    public virtual bool? IsActive { get; set; }
}

public class GetCustomerContactRequest
{
    public virtual int CustomerId { get; set; }
    public virtual int Id { get; set; }
}


