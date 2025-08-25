using Microsoft.EntityFrameworkCore;
using PayManager.Business.Contracts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Implementation.Service
{
    public class LinqService:ILinqService
    {
        public IQueryable<T> Filter<T>(IQueryable<T> query, string filterRaw)
        {
            throw new NotImplementedException();
        }
        public IQueryable<T> Sort<T>(IQueryable<T> query, string sortRaw)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Paginate<T>(IQueryable<T> query, int? skip, int? take)
        {
            return skip is null || take is null || take == 0 ? query : query.Skip(skip.Value).Take(take.Value);
        }

        public async Task<(IEnumerable<T> Data, int TotalCount)> PaginateAndGetData<T>(IQueryable<T> query, int? skip, int? take, Func<T, T> after = null)
        {
            var count = await query.CountAsync();
            query = Paginate(query, skip, take);
            var data = await query.ToListAsync();
            if (after is not null)
                data = data.Select(after).ToList();
            return (data, count);
        }

      
    }
}
