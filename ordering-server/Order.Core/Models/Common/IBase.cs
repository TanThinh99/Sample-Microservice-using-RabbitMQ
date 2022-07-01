namespace Order.Core.Models.Common
{
    public interface IBase
    {
        string Id { get; set; }

        int ClusteredKey { get; set; }
    }
}
