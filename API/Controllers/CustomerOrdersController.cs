using DotNetApi.Domain.Services;
using DotNetApi.Domain.Providers;
using DotNetApi.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DotNetApi.API.Controllers
{
    [Authorize]
    [Route("cutomers/{CustomerId}/orders")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly OrderService customerOrderService;
        private readonly OrderProvider customerOrderProvider;

        public CustomerOrdersController(
            OrderService customerOrderService,
            OrderProvider customerOrderProvider)
        {
            this.customerOrderService = customerOrderService;
            this.customerOrderProvider = customerOrderProvider;
        }

        [HttpGet]
        [Authorize(Roles = Constants.READER_ROLE)]
        public Task<Order[]> Get([FromQuery] CustomQueryCustomerOrderRequest request)
        {
            return this.customerOrderProvider.GetCustomerOrders(request);
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = Constants.READER_ROLE)]
        public Task<Order> Get([FromQuery] CustomGetCustomerOrderRequest request)
        {
            return this.customerOrderProvider.GetCustomerOrder(request);
        }

        [HttpPost]
        [Authorize(Roles = Constants.WRITER_ROLE)]
        public Task<Order> Post(int CustomerId, [FromBody] CreateOrderRequest request)
        {
            request.CustomerId = CustomerId;
            return this.customerOrderService.CreateOrder(request);
        }


        [HttpDelete("{Id}")]
        [Authorize(Roles = Constants.WRITER_ROLE)]
        public Task<Order> Delete([FromRoute] CancelCustomerOrderRequest request)
        {
            return this.customerOrderService.CancelCustomerOrder(request);
        }
    }

    public class CustomQueryCustomerOrderRequest : QueryCustomerOrderRequest
    {
        [FromRoute]
        public override int CustomerId { get; set; }
    }

    public class CustomGetCustomerOrderRequest : GetCustomerOrderRequest
    {
        [FromRoute]
        public override int CustomerId { get; set; }

        [FromRoute]
        public override int Id { get; set; }
    }
}
