using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Dal;
using Dork.Core.Domain;
using Dork.Core.Service;

namespace Dork.Service.Default.Impl
{
    public class EntityService<T> : IEntityService<T> where T : IEntity
    {
        public readonly IRepository<T> _repo;
        
        public EntityService(IRepository<T> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<long> CreateElementAsync(T entity)
        {
            return await _repo.UpdateAsync(entity);
        }

        public async Task<long> UpdateElementAsync(T entity)
        {
            return await _repo.UpdateAsync(entity);
        }

        public async Task<long> DeleteElementAsync(string id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
