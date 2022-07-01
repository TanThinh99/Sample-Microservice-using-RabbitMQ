using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Models;
using Order.Logic.Persistence;
using Order.Logic.Persistence.Dtos;
using ordering_server.RabbitMQ;

namespace ordering_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RabbitMqController : ControllerBase
    {
        private readonly OrderDbContext _context;

        private readonly IMapper _mapper;

        private readonly IMessageProducer _messagePushlisher;

        public RabbitMqController(OrderDbContext context, IMessageProducer messagePushlisher, IMapper mapper)
        {
            _context = context;
         
            _messagePushlisher = messagePushlisher;

            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(ProductTransactionDto dto)
        {
            ProductTransaction transaction = _mapper.Map<ProductTransaction>(dto);
            transaction.Status = "Ordering";
            transaction.CreatedDate = DateTime.Now;

            await _context.Transactions.AddAsync(transaction);

            await _context.SaveChangesAsync();

            _messagePushlisher.SendMessage(_mapper.Map<TransactionResponseDto>(transaction));

            return Ok(new { id = transaction.Id });
        }
    }
}
