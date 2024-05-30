using DotNetApi.Domain.DTOs;

namespace DotNetApi.Domain.Providers;

public interface CustomerContactProvider
{
    Task<Contact[]> GetCustomerContacts(QueryCustomerContactRequest request);
    Task<Contact> GetCustomerContact(GetCustomerContactRequest request);
}

public class QueryCustomerContactRequest
{
    public int CustomerId { get; set; }
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
}

public class GetCustomerContactRequest
{
    public int CustomerId { get; set; }
    public int Id { get; set; }
}


