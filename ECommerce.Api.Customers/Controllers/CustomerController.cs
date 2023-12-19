using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomersProvider customersProvider;

        public CustomerController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await customersProvider.GetCustomersAsync();
            if(result.IsSuccess)
            {
                return Ok(result.Customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();
        }
    }
}
