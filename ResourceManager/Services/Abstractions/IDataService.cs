using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ResourceManager.Services.Abstractions
{
    public interface IDataService<T>
    {
        Task<IList<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
        Task<IList<T>> GetWithRawSql(string query, params object[] parameters);
        Task<IList<T>> GetWhere(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);
        Task<T?> SingleOrDefault(Expression<Func<T, bool>> predicate);
    }
}
