using PayManager.Business.Contracts.ApplicationService;
using PayManager.Business.Domain;
using PayManager.DataAccess.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Implementation.ApplicationService
{
    public class ProductApplicationService(IProductRepository repository) : BaseApplicationService<Product>(repository, "products"), IProductApplicationService
    {
        protected IProductRepository Repository => (IProductRepository)BaseRepository;
    }
}
