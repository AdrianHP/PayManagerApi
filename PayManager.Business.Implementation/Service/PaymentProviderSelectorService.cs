using Microsoft.Extensions.Logging;
using PayManager.Business.Contracts.Service;
using PayManager.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Implementation.Service
{
    public class PaymentProviderSelectorService : IPaymentProviderSelectorService
    {
        private string _configuration;

        Dictionary<string, Func<PaymentMethod,double, double>> paymentProviders;
        public PaymentProviderSelectorService()
        {
            paymentProviders = new Dictionary<string, Func<PaymentMethod, double, double>>
            {
                { "PagaFacil", PagaFacil },
                {"CazaPagos",CazaPagos }
            };
        }

         double  PagaFacil(PaymentMethod paymentMethod,double amount)
        {
            switch (paymentMethod)
            {
                case PaymentMethod.Cash:
                    return amount + 15;
                case PaymentMethod.Card:
                    return amount + amount*0.01;
                case PaymentMethod.Transfer:
                    return amount;
                default:
                    return amount;
            }
        }

        double CazaPagos(PaymentMethod paymentMethod, double amount)
        {
            //there is an IVA here,I'm not going to take it into account for now, but it's just an addition.
            switch (paymentMethod)
            {
                case PaymentMethod.Cash:
                    return double.MaxValue;//It is not implemented in CazaPagos, that's why I avoid it.
                case PaymentMethod.Card:
                    double fee = amount <= 1500 ? amount*0.02 :
                                 amount <= 5000 ? amount * 0.015 :
                                                  amount * 0.005;
                    return amount + fee;
                case PaymentMethod.Transfer:
                    double feeT = amount <= 500 ? 5 :
                                  amount <= 1000 ? amount * 0.025 :
                                                   amount * 0.02;
                    return amount + feeT;
                default:
                    return amount;
            }
        }

        public async Task<string> SelectOptimalProviderAsync(PaymentMethod paymentMode, double amount)
        {
            return paymentProviders
                    .Select(p => new { Provider = p.Key, Value = p.Value(paymentMode,amount) })
                    .OrderBy(x => x.Value)
                    .First().Provider;
        }
    }
}
