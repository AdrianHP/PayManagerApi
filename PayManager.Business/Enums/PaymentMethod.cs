using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Enums
{
    public enum PaymentMethod
    {
        [Display(Name = "Cash")]
        Cash,
        [Display(Name = "Card")]
        Card,
        [Display(Name = "Transfer")]
        Transfer,
    }
}
