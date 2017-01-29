using Dork.Core.Domain;
using Dork.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dork.Service.Moc.Impl
{
    public class EntityServiceMoc<T> : IEntityService<T> where T : IEntity
    {
        public Task<long> CreateElementAsync(T entity)
        {
            return Task.Run(() => 12345L);
        }

        public Task<long> DeleteElementAsync(string id)
        {
            return Task.Run(() => 12345L);
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> list = new List<T>();
            return Task.Run(() => list);
        }

        public Task<T> GetByIdAsync(string id)
        {
            var t = default(T);
            return Task.Run(() => t);
        }

        public Task<long> UpdateElementAsync(T entity)
        {
            return Task.Run(() => 12345L);
        }
    }
}
