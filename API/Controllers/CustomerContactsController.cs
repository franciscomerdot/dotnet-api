using DotNetApi.Domain.Services;
using DotNetApi.Domain.Providers;
using DotNetApi.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
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
        public Task<Contact[]> Get([FromQuery] CustomQueryCustomerContactRequest request)
        {
            return this.customerContactProvider.GetCustomerContacts(request);
        }

        [HttpGet("{Id}")]
        public Task<Contact> Get([FromQuery] CustomGetCustomerContactRequest request)
        {
            return this.customerContactProvider.GetCustomerContact(request);
        }

        [HttpPost]
        public Task<Contact> Post([FromBody] CustomCreateCutomerContactRequest request)
        {
            return this.customerContactService.CreateContact(request);
        }

        [HttpPut("{Id}")]
        public Task<Contact> Put([FromBody] CustomUpdateCustomerContactRequest request)
        {
            return this.customerContactService.UpdateContact(request);
        }

        [HttpDelete("{Id}")]
        public Task Delete([FromRoute] CustomDeleteCustomerContactRequest request)
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

    public class CustomCreateCutomerContactRequest : CreateContactRequest
    {
        [FromRoute]
        public override int CustomerId { get; set; }
    }

    public class CustomUpdateCustomerContactRequest : UpdateContactRequest
    {
        [FromRoute]
        public override int CustomerId { get; set; }

        [FromRoute]
        public override int Id { get; set; }
    }

    public class CustomDeleteCustomerContactRequest : DeleteContactRequest
    {
        [FromRoute]
        public override int CustomerId { get; set; }

        [FromRoute]
        public override int Id { get; set; }
    }
}
