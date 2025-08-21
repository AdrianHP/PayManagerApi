using PayManager.Business.Domain;
using PayManager.DataAccess.Contracts;
using PayManager.DataAccess.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.DataAccess.Implementation.Repository
{
    public class PaymentOrderRepository(IObjectContext context) : BaseRepository<PaymentOrder, Guid>(context), IPaymentOrderRepository
    {
        public Task<bool> CancelOrderAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

       

        public Task<bool> PayOrderAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        async Task CreateOrder(PaymentOrder order)
        {
            await AddAsync(order);
        }

        async Task IPaymentOrderRepository.CreateOrder(PaymentOrder order)
        {
            await AddAsync(order);
        }
    }
}
