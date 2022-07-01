namespace ReceiveEndpoint.Persistence.Dtos
{
    public class TransactionResponseDto
    {
        public string Id { get; set; }

        public string pId { get; set; }

        public int Quantity { get; set; }

        public string Status { get; set; }
    }
}
