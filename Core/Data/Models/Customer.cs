namespace DotNetApi.Core.Data.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}

