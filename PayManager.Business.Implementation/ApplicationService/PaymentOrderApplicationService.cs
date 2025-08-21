using AutoMapper;
using PayManager.ApiService;
using PayManager.ApiService.Models;
using PayManager.Business.Contracts.ApplicationService;
using PayManager.Business.Contracts.Service;
using PayManager.Business.Domain;
using PayManager.Business.Enums;
using PayManager.DataAccess.Contracts.Repository;
using System;
using PayManager.Business.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Implementation.ApplicationService
{
    public class PaymentOrderApplicationService(
        IPaymentOrderRepository repository,
        IPaymentProviderApiService providerApiService,
        IOrderProductApplicationService orderProductsApplicationService,
        IMapper mapper) : BaseApplicationService<PaymentOrder>(repository, "PaymentOrders"), IPaymentOrderApplicationService
    {
        protected IPaymentOrderRepository Repository => (IPaymentOrderRepository)BaseRepository;

        public Task<bool> CancelOrderAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderResponse> CreateOrder(PaymentOrder order, IEnumerable<Product> products)
        {
            
            var amount = products.Sum(x => x.UnitPrice);
            var optimalProvider = GetOptimalProvider(amount , order.PaymentMethod);

            var orderCreateModel = new OrderCreateModel
            {
                Method = order.PaymentMethod.GetDisplayName(),
                Products = products.Select(x => new ProductModel() { Name = x.Name, UnitPrice = x.UnitPrice })
            };
           var orderResponse = await providerApiService.CreateOrder(orderCreateModel,optimalProvider);
            //var orderRecord = mapper.Map<PaymentOrder>(orderResponse);
            var orderRecord = new PaymentOrder()
            {
                PaymentMethod = Enum.Parse<PaymentMethod>(orderResponse.Method),
                OrderStatus = Enum.Parse<OrderStatus>(orderResponse.Status),
                FeesAmount = orderResponse.Fees != null ? orderResponse.Fees.Sum(f => f.Amount) : 0.0,
            };
            foreach (var item in products)
            {
                orderProductsApplicationService.Add(new OrderProduct
                {
                    ProductId = item.Id,
                    PaymentOrderId = orderRecord.Id,
                });
            }
            await repository.CreateOrder(orderRecord);
            return orderResponse;
        }

        private string GetOptimalProvider(double amount, PaymentMethod paymentMethod) 
        {
            return "PagaFacil";
        }

        public Task<bool> PayOrderAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

    }
}
