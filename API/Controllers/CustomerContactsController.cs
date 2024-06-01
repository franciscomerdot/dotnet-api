using DotNetApi.Domain.Services;
using DotNetApi.Domain.Providers;
using DotNetApi.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DotNetApi.API.Controllers
{
    [Authorize]
    [Route("cutomers/{CustomerId}/contacts")]
    [ApiController]
    public class CustomerContactsController : ControllerBase
    {
        private readonly CustomerContactService customerContactService;
        private readonly CustomerContactProvider customerContactProvider;

        public CustomerContactsController(
                CustomerContactService customerContactService,
            CustomerContactProvider customerContactProvider)
        {
            this.customerContactService = customerContactService;
            this.customerContactProvider = customerContactProvider;
        }

        [HttpGet()]
        [Authorize(Roles = Constants.READER_ROLE)]
        public Task<Contact[]> Get([FromQuery] CustomQueryCustomerContactRequest request)
        {
            return this.customerContactProvider.QueryCustomerContacts(request);
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = Constants.READER_ROLE)]
        public Task<Contact> Get([FromQuery] CustomGetCustomerContactRequest request)
        {
            return this.customerContactProvider.GetCustomerContact(request);
        }

        [HttpPost]
        [Authorize(Roles = Constants.WRITER_ROLE)]
        public Task<Contact> Post(int CustomerId, [FromBody] CreateContactRequest request)
        {
            request.CustomerId = CustomerId;
            return this.customerContactService.CreateContact(request);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = Constants.WRITER_ROLE)]
        public Task<Contact> Put(int CustomerId, int Id, [FromBody] UpdateContactRequest request)
        {
            request.CustomerId = CustomerId;
            request.Id = Id;
            return this.customerContactService.UpdateContact(request);
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = Constants.WRITER_ROLE)]
        public Task Delete([FromRoute] DeleteContactRequest request)
        {
            return this.customerContactService.DeleteContact(request);
        }
    }

    public class CustomQueryCustomerContactRequest : QueryCustomerContactRequest
    {
        [FromRoute]
        public override int CustomerId { get; set; }
    }

    public class CustomGetCustomerContactRequest : GetCustomerContactRequest
    {
        [FromRoute]
        public override int CustomerId { get; set; }

        [FromRoute]
        public override int Id { get; set; }
    }
}
