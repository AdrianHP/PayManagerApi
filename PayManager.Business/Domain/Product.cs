using PayManager.Business.Domain;

namespace PayManager.Business.Domain
{
    public class Product : TrackableEntity
    {
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public bool IsActive { get; set; }
        public int UnitsInStock { get; set; }
    }
}
