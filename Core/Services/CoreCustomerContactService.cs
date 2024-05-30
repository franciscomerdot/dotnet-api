using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Services;

namespace DotNetApi.Core.Services;

public class CoreCustomerContactService : CustomerContactService
{
    public Task<Contact> CreateContact(CreateContactRequest request)
    {
        return Task.FromResult(new Contact());
    }

    public Task<Contact> UpdateContact(UpdateContactRequest request)
    {
        return Task.FromResult(new Contact());
    }

    public Task<Contact> EnableContact(EnableContactRequest request)
    {
        return Task.FromResult(new Contact());
    }

    public Task<Contact> DisableContact(DisableContactRequest request)
    {
        return Task.FromResult(new Contact());
    }

    public Task<Contact> MakePrimaryContact(MakePrimaryContactRequest request)
    {
        return Task.FromResult(new Contact());
    }

    public Task DeleteContact(DeleteContactRequest request)
    {
        return Task.CompletedTask;
    }
}

