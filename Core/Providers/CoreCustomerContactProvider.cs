using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Providers;
using DotNetApi.Core.Data;

namespace DotNetApi.Core.Providers;

public class CoreCustomerContactProvider : CustomerContactProvider
{
    private readonly IMapper mapper;
    private readonly DataContext dataContext;

    public CoreCustomerContactProvider(IMapper mapper, DataContext dataContext)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Contact[]> QueryCustomerContacts(QueryCustomerContactRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request.CustomerId <= 0)
        {
            throw new ArgumentException("CustomerId is required", nameof(request.CustomerId));
        }

        var query = this.dataContext.Contacts
            .Where(x => x.CustomerId == request.CustomerId)
            .OrderBy(x => x.Id)
            .AsQueryable();

        if (request.Name != null)
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(x => x.IsActive == request.IsActive.Value);
        }

        return this.mapper.Map<Contact[]>(await query.ToArrayAsync());
    }

    public async Task<Contact> GetCustomerContact(GetCustomerContactRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request.CustomerId <= 0)
        {
            throw new ArgumentException("CustomerId is required", nameof(request.CustomerId));
        }

        if (request.Id <= 0)
        {
            throw new ArgumentException("Id is required", nameof(request.Id));
        }

        var query = this.dataContext.Contacts
            .Include(x => x.Customer)
            .Where(x => x.CustomerId == request.CustomerId && x.Id == request.Id)
            .AsQueryable();

        var contact = await query.FirstOrDefaultAsync();

        if (contact == null)
        {
            throw new ArgumentException("Contact not found", nameof(request.Id));
        }

        return this.mapper.Map<Contact>(contact);
    }
}

