using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Domain;

namespace Dork.Core.Service
{
    public interface IEntityService<T> where T : IEntity
    {
        Task<T> GetByIdAsync(string id);

        Task<long> CreateElementAsync(T entity);
        Task<long> UpdateElementAsync(T entity);

        Task<long> DeleteElementAsync(string id);
    }
}
