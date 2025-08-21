using PayManager.Business.Domain;
using PayManager.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Contracts.Service
{
    //not stored in database
    public interface IPaymentProviderSelector
    {
        Task<string> SelectOptimalProviderAsync(PaymentMethod paymentMode, decimal amount);
    }
}
