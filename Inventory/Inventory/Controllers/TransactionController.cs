using Inventory.Models;
using Inventory.Persistence;
using Inventory.Persistence.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public TransactionController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult UpdateTransactionStatus(TransactionResponseDto response)
        {
            ProductTransaction transaction = _context.Transactions.Find(response.Id);

            if (transaction == null)
            {
                return NotFound();
            }
            else
            {
                transaction.Status = response.Status;

                _context.Entry(transaction).State = EntityState.Modified;

                _context.SaveChanges();

                return Ok();
            }
        }
    }
}
