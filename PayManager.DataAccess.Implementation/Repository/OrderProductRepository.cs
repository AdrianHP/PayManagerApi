using PayManager.Business.Domain;
using PayManager.DataAccess.Contracts;
using PayManager.DataAccess.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.DataAccess.Implementation.Repository
{
    public class OrderProductRepository(IObjectContext context) : BaseRepository<OrderProduct, Guid>(context), IOrderProductRepository
    {
    }
}
