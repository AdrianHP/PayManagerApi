using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Domain
{
    public class OrderProduct:TrackableEntity
    {
        public Guid PaymentOrderId { get; set; }
        public PaymentOrder PaymentOrder { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

    }
}
