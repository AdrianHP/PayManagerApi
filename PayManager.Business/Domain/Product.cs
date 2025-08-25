using PayManager.Business.Domain;

namespace PayManager.Business.Domain
{
    public class Product : TrackableEntity
    {
        public string Name { get; set; } = "";
        public double UnitPrice { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public int UnitsInStock { get; set; } = 0;
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
