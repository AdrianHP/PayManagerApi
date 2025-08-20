using PayManager.Business.Domain;
using PayManager.Business.Domain.NotMapped;
using PayManager.Business.Enums;

namespace PayManager.Business.Domain
{
    public class PaymentOrder: TrackableEntity
    {
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; } 
        public string ProviderName { get; set; }
        public string ProviderOrderId { get; set; } 
        public OrderStatus OrderStatus { get; set; }
        public decimal  FeesAmount { get; set; } 
        public List<Product> Products { get; set; } 
       
    }
}
