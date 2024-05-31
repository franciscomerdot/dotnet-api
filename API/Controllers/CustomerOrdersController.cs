using DotNetApi.Domain.Services;
using DotNetApi.Domain.Providers;
using DotNetApi.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
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
        public Task<Order[]> Get([FromQuery] CustomQueryCustomerOrderRequest request)
        {
            return this.customerOrderProvider.GetCustomerOrders(request);
        }

        [HttpGet("{Id}")]
        public Task<Order> Get([FromQuery] CustomGetCustomerOrderRequest request)
        {
            return this.customerOrderProvider.GetCustomerOrder(request);
        }

        [HttpPost]
        public Task<Order> Post(int CustomerId, [FromBody] CreateOrderRequest request)
        {
            request.CustomerId = CustomerId;
            return this.customerOrderService.CreateOrder(request);
        }


        [HttpDelete("{Id}")]
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
