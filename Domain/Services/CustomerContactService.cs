using DotNetApi.Domain.DTOs;

namespace DotNetApi.Domain.Services;

public interface CustomerContactService
{
    Task<Contact> CreateContact(CreateContactRequest request);
    Task<Contact> UpdateContact(UpdateContactRequest request);
    Task<Contact> EnableContact(EnableContactRequest request);
    Task<Contact> DisableContact(DisableContactRequest request);
    Task<Contact> MakePrimaryContact(MakePrimaryContactRequest request);
    Task DeleteContact(DeleteContactRequest request);
}

public class CreateContactRequest
{
    public virtual int CustomerId { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual string Email { get; set; } = default!;
    public virtual string Phone { get; set; } = default!;
    public virtual bool IsPrimary { get; set; }
}

public class UpdateContactRequest
{
    public virtual int CustomerId { get; set; }
    public virtual int Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual string Email { get; set; } = default!;
    public virtual string Phone { get; set; } = default!;
}

public class EnableContactRequest
{
    public virtual int CustomerId { get; set; }
    public virtual int Id { get; set; }
}

public class DisableContactRequest
{
    public virtual int CustomerId { get; set; }
    public virtual int Id { get; set; }
}

public class MakePrimaryContactRequest
{
    public virtual int CustomerId { get; set; }
    public virtual int Id { get; set; }
}

public class DeleteContactRequest
{
    public virtual int CustomerId { get; set; }
    public virtual int Id { get; set; }
}
