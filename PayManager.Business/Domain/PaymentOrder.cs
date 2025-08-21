using PayManager.Business.Domain;
using PayManager.Business.Domain.NotMapped;
using PayManager.Business.Enums;

namespace PayManager.Business.Domain
{
    public class PaymentOrder: TrackableEntity
    {
        public double Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; } 
        public string ProviderName { get; set; }
        public string ProviderOrderId { get; set; } 
        public OrderStatus OrderStatus { get; set; }
        public double  FeesAmount { get; set; } 
        public ICollection<Product> Products { get; set; } 
       
    }
}
