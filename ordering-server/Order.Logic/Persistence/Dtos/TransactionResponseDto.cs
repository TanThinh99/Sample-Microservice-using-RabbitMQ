using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Logic.Persistence.Dtos
{
    public class TransactionResponseDto
    {
        public string Id { get; set; }

        public string pId { get; set; }

        public int Quantity { get; set; }

        public string Status { get; set; }
    }
}
