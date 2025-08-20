using PayManager.Business.Contracts.ApplicationService;
using PayManager.Business.Domain;
using PayManager.DataAccess.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Implementation.ApplicationService
{
    public class PaymentOrderApplicationService(IPaymentOrderRepository repository) : BaseApplicationService<PaymentOrder>(repository, "PaymentOrders"), IPaymentOrderApplicationService
    {
        protected IPaymentOrderRepository Repository => (IPaymentOrderRepository)BaseRepository;

        public Task<bool> CancelOrderAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PayOrderAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
