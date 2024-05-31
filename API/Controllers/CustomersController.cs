using DotNetApi.Domain.Services;
using DotNetApi.Domain.Providers;
using DotNetApi.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService customerService;
        private readonly CustomerProvider customerProvider;

        public CustomersController(
            CustomerService customerService,
            CustomerProvider customerProvider)
        {
            this.customerService = customerService;
            this.customerProvider = customerProvider;
        }

        [HttpGet]
        public Task<Customer[]> Get([FromQuery] QueryCustomerRequest request)
        {
            return this.customerProvider.GetCustomers(request);
        }

        [HttpGet("{Id}")]
        public Task<Customer> Get([FromQuery] CustomGetCustomerRequest request)
        {
            return this.customerProvider.GetCustomer(request);
        }

        [HttpPost]
        public Task<Customer> Post([FromBody] CreateCustomerRequest request)
        {
            return this.customerService.CreateCustomer(request);
        }

        [HttpPut("{Id}")]
        public Task<Customer> Put(int Id, [FromBody] UpdateCustomerRequest request)
        {
            request.Id = Id;
            return this.customerService.UpdateCustomer(request);
        }

        [HttpDelete("{Id}")]
        public Task Delete([FromRoute] DeleteCustomerRequest request)
        {
            return this.customerService.DeleteCustomer(request);
        }
    }

    public class CustomGetCustomerRequest : GetCustomerRequest
    {
        [FromRoute]
        public override int Id { get; set; }
    }
}
