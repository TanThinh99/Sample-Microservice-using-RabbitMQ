namespace Inventory.Models.Common
{
    public interface IDate : IBase
    {
        DateTimeOffset CreatedDate { get; set; }

        DateTimeOffset? ModifiedDate { get; set; }
    }
}
