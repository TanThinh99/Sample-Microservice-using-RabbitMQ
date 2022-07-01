namespace Inventory.Models.Common
{
    public class Date : Base, IDate
    {
        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
