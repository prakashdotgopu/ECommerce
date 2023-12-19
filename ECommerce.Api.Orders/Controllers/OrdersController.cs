using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrdersProvider ordersProvider;
        
        public OrdersController(IOrdersProvider ordersProvider) 
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrders(int customerId)
        {
            var result = await ordersProvider.GetOrdersAsync(customerId);
            if(result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}
