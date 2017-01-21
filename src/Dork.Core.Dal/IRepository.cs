using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dork.Core.Domain;

namespace Dork.Core.Dal
{
    public interface IRepository<T, in Key> : IQueryable<T>, IRepository where T : IEntity<Key>
    {
        
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> condition);

        Task<T> GetByIdAsync(Key id);

        Task<long> UpdateAsync(T entity);
        Task<long> UpdateAsync(IEnumerable<T> entities);

        Task<long> DeleteAsync(Key id);
        Task<long> DeleteAsync(T entity);
        Task<long> DeleteAllAsync();

        Task<long> CountAsync();
    }

    public interface IRepository<T> : IRepository<T, string> where T : IEntity<string> { }

    public interface IRepository { }
}
