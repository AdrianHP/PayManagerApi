using PayManager.Business.Domain;

namespace PayManager.DataAccess.Contracts.Repository
{
    public interface IPaymentOrderRepository: IRepository<PaymentOrder>
    {
        Task CreateOrder(PaymentOrder order);
        Task<bool> CancelOrderAsync(Guid Id);
        Task<bool> PayOrderAsync(Guid Id);
    }
}
