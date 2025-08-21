using PayManager.ApiService.Models;
using PayManager.Business.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Contracts.ApplicationService
{
    public interface IPaymentOrderApplicationService : IApplicationService<PaymentOrder>
    {

        Task <OrderResponse> CreateOrder(PaymentOrder order,IEnumerable<Product> products);
        Task<bool> CancelOrderAsync(Guid Id);
        Task<bool> PayOrderAsync(Guid Id);
    }
}
