using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Implementation.DTOs
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }
        public int UnitsInStock { get; set; }
    }
}
