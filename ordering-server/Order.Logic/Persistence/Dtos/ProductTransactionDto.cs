namespace Order.Logic.Persistence.Dtos
{
    public class ProductTransactionDto
    {
        public string cId { get; set; }

        public string pId { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public DateTimeOffset OrderDate { get; set; }
    }
}
