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
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public bool IsPrimary { get; set; }
}

public class UpdateContactRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
}

public class EnableContactRequest
{
    public int Id { get; set; }
}

public class DisableContactRequest
{
    public int Id { get; set; }
}

public class MakePrimaryContactRequest
{
    public int Id { get; set; }
}

public class DeleteContactRequest
{
    public int Id { get; set; }
}
