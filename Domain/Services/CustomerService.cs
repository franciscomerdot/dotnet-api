using DotNetApi.Domain.DTOs;

namespace DotNetApi.Domain.Services;

public interface CustomerService
{
    Task<Customer> CreateCustomer(CreateCustomerRequest request);
    Task<Customer> UpdateCustomer(UpdateCustomerRequest request);
    Task<Customer> EnableCustomer(EnableCustomerRequest request);
    Task<Customer> DisableCustomer(DisableCustomerRequest request);
    Task DeleteCustomer(DeleteCustomerRequest request);
}

public class CreateCustomerRequest
{
    public string Name { get; set; } = default!;
}

public class UpdateCustomerRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}

public class EnableCustomerRequest
{
    public int Id { get; set; }
}

public class DisableCustomerRequest
{
    public int Id { get; set; }
}

public class DeleteCustomerRequest
{
    public int Id { get; set; }
}
