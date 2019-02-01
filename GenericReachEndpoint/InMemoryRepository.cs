using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericReachEndpoint
{
    public class InMemoryRepository : IRepository
    { 
        List<object> list = new List<object>();

        public async Task<IEnumerable<T>> GetItemsAsync<T, TKey>(Expression<Func<T, bool>> func, Expression<Func<T, TKey>> orderBy)
        {
            return list.Where(x => x is T)
                       .Cast<T>()
                       .Where(func.Compile())
                       .OrderBy(orderBy.Compile());
        }

        public async Task CreateItemAsync<T>(T item)
        {
            list.Add(item);
        }
    }
}