using PayManager.Business.Domain;
using PayManager.Business.Domain.NotMapped;
using PayManager.Business.Enums;
using PayManager.Business.Implementation.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Implementation.DTOs
{
    public class PaymentOrderDTO: BaseDTO<Guid>
    {
        public double? Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string? ProviderName { get; set; }
        public string? ProviderOrderId { get; set; }
        public string? OrderStatus { get; set; } 
        public ICollection<FeeDTO> Fees { get; set; }
        public ICollection<ProductDTO> Products { get; set; } = [];
    }
}
