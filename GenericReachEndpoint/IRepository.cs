using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericReachEndpoint
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetItemsAsync<T, TKey>(Expression<Func<T, bool>> expression, Expression<Func<T, TKey>> orderBy);
        Task CreateItemAsync<T>(T item);
    }
}