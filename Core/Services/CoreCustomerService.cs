using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DotNetApi.Domain.DTOs;
using DotNetApi.Domain.Services;
using DotNetApi.Core.Data;
using DataModel = DotNetApi.Core.Data.Models;

namespace DotNetApi.Core.Services;

public class CoreCustomerService : CustomerService
{
    private readonly IMapper mapper;
    private readonly DataContext dataContext;


    public CoreCustomerService(IMapper mapper, DataContext dataContext)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Customer> CreateCustomer(CreateCustomerRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required", nameof(request.Name));
        }

        var customer = new DataModel.Customer
        {
            Name = request.Name,
            IsActive = true,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow
        };

        await this.dataContext.Customers.AddAsync(customer);

        this.dataContext.SaveChanges();

        return this.mapper.Map<Customer>(customer);
    }

    private async Task<DataModel.Customer> GetCustomerById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id is required", "Id");
        }

        var customer = await this.dataContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

        if (customer == null)
        {
            throw new ArgumentException("Customer not found", nameof(id));
        }

        return customer;
    }

    public async Task<Customer> UpdateCustomer(UpdateCustomerRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required", nameof(request.Name));
        }

        var customer = await this.GetCustomerById(request.Id);

        customer.Name = request.Name;

        this.dataContext.SaveChanges();

        return this.mapper.Map<Customer>(customer);
    }

    public async Task<Customer> EnableCustomer(EnableCustomerRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var customer = await this.GetCustomerById(request.Id);

        customer.IsActive = true;

        this.dataContext.SaveChanges();

        return this.mapper.Map<Customer>(customer);
    }

    public async Task<Customer> DisableCustomer(DisableCustomerRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var customer = await this.GetCustomerById(request.Id);

        customer.IsActive = false;

        this.dataContext.SaveChanges();

        return this.mapper.Map<Customer>(customer);
    }

    public async Task DeleteCustomer(DeleteCustomerRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var customer = await this.GetCustomerById(request.Id);

        customer.IsDeleted = true;

        this.dataContext.SaveChanges();
    }
}

