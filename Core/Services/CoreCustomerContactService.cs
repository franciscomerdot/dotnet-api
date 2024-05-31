using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Services;
using DotNetApi.Core.Data;
using DataModel = DotNetApi.Core.Data.Models;

namespace DotNetApi.Core.Services;

public class CoreCustomerContactService : CustomerContactService
{
    private readonly IMapper mapper;
    private readonly DataContext dataContext;

    public CoreCustomerContactService(IMapper mapper, DataContext dataContext)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    private async Task<DataModel.Contact> GetContactById(int customerId, int id)
    {
        if (customerId <= 0)
        {
            throw new ArgumentException("CustomerId is required", "CustomerId");
        }

        if (id <= 0)
        {
            throw new ArgumentException("Id is required", "Id");
        }

        var contact = await this.dataContext
            .Contacts
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.Id == id);

        if (contact == null)
        {
            throw new ArgumentException("Contact not found", nameof(id));
        }

        return contact;
    }

    public async Task<Contact> CreateContact(CreateContactRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required", nameof(request.Name));
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ArgumentException("Email is required", nameof(request.Email));
        }

        if (string.IsNullOrWhiteSpace(request.Phone))
        {
            throw new ArgumentException("Phone is required", nameof(request.Phone));
        }

        if (request.CustomerId <= 0)
        {
            throw new ArgumentException("CustomerId is required", nameof(request.CustomerId));
        }

        var customer = await this.dataContext.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId);

        if (customer == null)
        {
            throw new ArgumentException("Customer not found", nameof(request.CustomerId));
        }

        var contact = new DataModel.Contact
        {
            CustomerId = request.CustomerId,
            Customer = customer,
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            IsActive = true,
            IsDeleted = false,
            IsPrimary = request.IsPrimary,
            CreatedAt = DateTime.UtcNow
        };

        await this.dataContext.Contacts.AddAsync(contact);

        if (request.IsPrimary)
        {
            var currentPrimaryContact = await this.dataContext.Contacts.FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId && x.IsPrimary);

            if (currentPrimaryContact != null)
            {
                currentPrimaryContact.IsPrimary = false;
            }
        }

        this.dataContext.SaveChanges();

        return this.mapper.Map<Contact>(contact);
    }

    public async Task<Contact> UpdateContact(UpdateContactRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var contact = await GetContactById(request.CustomerId, request.Id);

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            contact.Name = request.Name;
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            contact.Email = request.Email;
        }

        if (!string.IsNullOrWhiteSpace(request.Phone))
        {
            contact.Phone = request.Phone;
        }

        this.dataContext.SaveChanges();

        return this.mapper.Map<Contact>(contact);
    }

    public async Task<Contact> EnableContact(EnableContactRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var contact = await GetContactById(request.CustomerId, request.Id);

        contact.IsActive = true;

        this.dataContext.SaveChanges();

        return this.mapper.Map<Contact>(contact);
    }

    public async Task<Contact> DisableContact(DisableContactRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var contact = await GetContactById(request.CustomerId, request.Id);

        contact.IsActive = false;

        this.dataContext.SaveChanges();

        return this.mapper.Map<Contact>(contact);
    }

    public async Task<Contact> MakePrimaryContact(MakePrimaryContactRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var currentPrimaryContact = await this.dataContext.Contacts.FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId && x.IsPrimary);

        if (currentPrimaryContact != null && currentPrimaryContact.Id != request.Id)
        {
            currentPrimaryContact.IsPrimary = false;
        }

        var contact = await GetContactById(request.CustomerId, request.Id);

        contact.IsPrimary = true;

        this.dataContext.SaveChanges();

        return this.mapper.Map<Contact>(contact);
    }

    public async Task DeleteContact(DeleteContactRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var contact = await GetContactById(request.CustomerId, request.Id);

        contact.IsDeleted = true;

        this.dataContext.SaveChanges();
    }
}

