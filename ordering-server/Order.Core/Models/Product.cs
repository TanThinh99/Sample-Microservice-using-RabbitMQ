using Order.Core.Models.Common;

namespace Order.Core.Models
{
    public class Product : Date
    {
        public string Name { get; set; }

        public int QOH { get; set; }
    }
}
