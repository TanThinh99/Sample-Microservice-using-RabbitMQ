using Microsoft.AspNetCore.Mvc;
using Order.Core.Models;
using Order.Logic.Persistence;
using Order.Logic.Persistence.Dtos;

namespace ordering_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public CustomerController(OrderDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetCustomers()
        {
            return Ok(_context.Customers.ToList());
        }

        [HttpPost]
        public ActionResult<Product> AddCustomer([FromBody] UpdateCustomerDto dto)
        {
            Customer customer = new Customer()
            {
                Name = dto.Name,
            };

            _context.Customers.Add(customer);

            _context.SaveChanges();

            return Ok(customer);
        }
    }
}
