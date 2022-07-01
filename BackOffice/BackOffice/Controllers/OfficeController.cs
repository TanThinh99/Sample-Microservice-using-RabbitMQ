using BackOffice.Models;
using BackOffice.Persistence;
using BackOffice.Persistence.Dtos;
using BackOffice.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfficeController : ControllerBase
    {
        private readonly BackOfficeDbContext _context;

        private readonly IMessageProducer _messageProducer;

        public OfficeController(BackOfficeDbContext context, IMessageProducer messageProducer)
        {
            _context = context;

            _messageProducer = messageProducer;
        }

        [HttpPost]
        [Route("check-product")]
        public IActionResult CheckProduct([FromBody] TransactionResponseDto dto)
        {
            Product product = _context.Products.Find(dto.pId);
            
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                //TransactionResponseDto response = new TransactionResponseDto()
                //{
                //    Id = dto.Id,
                //};
                if (dto.Quantity > product.QOH)
                {
                    dto.Status = "Rejected";
                }
                else
                {
                    // update 
                    product.QOH = product.QOH - dto.Quantity;
                    _context.Entry(product).State = EntityState.Modified;
                    _context.SaveChanges();

                    dto.Status = "Confirmed";
                }
                _messageProducer.SendMessage(dto);

                return Ok();
            }
        }
    }
}
