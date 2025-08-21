using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.ApiService.Models
{
    public class OrderCreateModel
    {
        public string Method { get; set; }
        public IEnumerable<ProductModel> Products { get; set; }
    } 
}
