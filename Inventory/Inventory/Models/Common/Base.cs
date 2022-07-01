namespace Inventory.Models.Common
{
    public class Base : IBase
    {
        public string Id { get; set; }

        public int ClusteredKey { get; set; }
    }
}
