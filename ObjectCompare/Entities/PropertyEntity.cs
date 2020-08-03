namespace ObjectCompare.Entities
{
    public interface IProperty
    {
        string Acreage { get; }
        string AddressLine1 { get; }
        string AddressLine2 { get; }
        string Block { get; }
        string City { get; }
        string County { get; }
        int EntityId { get; }
        string LegalDescription { get; }
        string Phase { get; }
        string State { get; }
        string Zip { get; }
    }

    public class PropertyEntity : IProperty
    {
        public int EntityId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string LegalDescription { get; set; }
        public string Acreage { get; set; }
        public string Block { get; set; }
        public string Phase { get; set; }
    }
}
