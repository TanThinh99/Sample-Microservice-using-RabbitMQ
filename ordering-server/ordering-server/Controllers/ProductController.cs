using Microsoft.AspNetCore.Mvc;
using Order.Core.Models;
using Order.Logic.Persistence;
using Order.Logic.Persistence.Dtos;

namespace ordering_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public ProductController(OrderDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_context.Products.ToList());
        }

        [HttpPost]
        public ActionResult<Product> AddProduct([FromBody] UpdateProductDto dto)
        {
            Product product = new Product()
            {
                Name = dto.Name,
                QOH = dto.QOH,
            };

            _context.Products.Add(product);

            _context.SaveChanges();

            return Ok(product);
        }
    }
}
