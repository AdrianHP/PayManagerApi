using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Created")]
        Created,
        [Display(Name = "Pending")]
        Pending,
        [Display(Name = "Paid")]
        Paid,
        [Display(Name = "Cancelled")]
        Cancelled,
    }
}
