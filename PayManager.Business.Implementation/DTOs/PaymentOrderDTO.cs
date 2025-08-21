using PayManager.Business.Domain;
using PayManager.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Implementation.DTOs
{
    public class PaymentOrderDTO
    {
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ProviderName { get; set; }
        public string ProviderOrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal FeesAmount { get; set; }
        public ICollection<ProductDTO> Products { get; set; }
    }
}
