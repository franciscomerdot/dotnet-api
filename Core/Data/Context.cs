using DotNetApi.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetApi.Core.Data;

public class DataContext : DbContext
{
    public virtual DbSet<Customer> Customers { get; set; } = default!;
    public virtual DbSet<Order> Orders { get; set; } = default!;
    public virtual DbSet<Contact> Contacts { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        optionsBuilder.UseNpgsql(connectionUrl);
    }
}
