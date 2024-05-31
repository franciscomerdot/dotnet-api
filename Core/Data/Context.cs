using DotNetApi.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetApi.Core.Data;

public class DataContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Contact> Contacts { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        optionsBuilder.UseNpgsql(connectionUrl);
    }
}
