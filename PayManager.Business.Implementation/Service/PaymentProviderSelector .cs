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
    public class PaymentProviderSelector : IPaymentProviderSelector
    {
        private string _configuration;

        public PaymentProviderSelector(string configuration)
        {
            var a = 3;
            _configuration = configuration;
        }

        public async Task<string> SelectOptimalProviderAsync(PaymentMethod paymentMode, double amount)
        {
            return "PagaFacil"; 
        }
    }
}
