using DotNetApi.Domain.Services;
using DotNetApi.Domain.Providers;
using DotNetApi.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DotNetApi.API.Controllers
{
    [Authorize]
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
        [Authorize(Roles = Constants.READER_ROLE)]
        public Task<Customer[]> Get([FromQuery] QueryCustomerRequest request)
        {
            return this.customerProvider.QueryCustomers(request);
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = Constants.READER_ROLE)]
        public Task<Customer> Get([FromQuery] CustomGetCustomerRequest request)
        {
            return this.customerProvider.GetCustomer(request);
        }

        [HttpPost]
        [Authorize(Roles = Constants.WRITER_ROLE)]
        public Task<Customer> Post([FromBody] CreateCustomerRequest request)
        {
            return this.customerService.CreateCustomer(request);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = Constants.WRITER_ROLE)]
        public Task<Customer> Put(int Id, [FromBody] UpdateCustomerRequest request)
        {
            request.Id = Id;
            return this.customerService.UpdateCustomer(request);
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = Constants.WRITER_ROLE)]
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
