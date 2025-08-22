using PayManager.ApiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.ApiService
{
    public interface IPaymentProviderApiService
    {
        Task<IEnumerable<OrderResponse>> GetOrders();
        Task<OrderResponse> CreateOrder(OrderCreateModel order, string provider);
        Task PayOrder(string orderId, string provider);
        Task CancelOrder(string orderId, string provider);
    }
}
