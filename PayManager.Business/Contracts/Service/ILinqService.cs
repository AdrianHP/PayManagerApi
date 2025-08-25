using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Contracts.Service
{
    public interface ILinqService
    {
        IQueryable<T> Filter<T>(IQueryable<T> query, string filterRaw);
        IQueryable<T> Sort<T>(IQueryable<T> query, string sortRaw);
        IQueryable<T> Paginate<T>(IQueryable<T> query, int? skip, int? take);
        Task<(IEnumerable<T> Data, int TotalCount)> PaginateAndGetData<T>(IQueryable<T> query, int? skip, int? take, Func<T, T> after = null);
    }
}
