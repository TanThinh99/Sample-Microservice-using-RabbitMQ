using Inventory.Models.Common;

namespace Inventory.Models
{
    public class ProductTransaction : Date
    {
        public string cId { get; set; }

        public string pId { get; set; }

        public int Quantity { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public DateTimeOffset OrderDate { get; set; }
    }
}
