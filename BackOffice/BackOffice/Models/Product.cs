namespace BackOffice.Models
{
    public class Product
    {
        public string Id { get; set; }

        public int ClusteredKey { get; set; }

        public string Name { get; set; }

        public int QOH { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
