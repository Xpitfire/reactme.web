using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Domain;

namespace Dork.Core.Service
{
    public interface IEntityService<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);

        Task<long> CreateElement(T entity);
        Task<long> UpdateElement(T entity);

        Task<long> DeleteElement(string id);
    }
}
