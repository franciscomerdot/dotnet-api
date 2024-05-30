using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Providers;

namespace DotNetApi.Core.Providers;

public class CoreCustomerContactProvider : CustomerContactProvider
{
    public Task<Contact[]> GetCustomerContacts(QueryCustomerContactRequest request)
    {
        return Task.FromResult(new Contact[0]);
    }

    public Task<Contact> GetCustomerContact(GetCustomerContactRequest request)
    {
        return Task.FromResult(new Contact());
    }
}

