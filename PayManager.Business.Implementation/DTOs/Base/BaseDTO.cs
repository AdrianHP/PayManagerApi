using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Implementation.DTOs.Base
{
    public class BaseDTO<T>
    {
        public T Id { get; set; }
    }
}
