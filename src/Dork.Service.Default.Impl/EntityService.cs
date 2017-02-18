using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Dal;
using Dork.Core.Domain;
using Dork.Core.Service;
using MongoDB.Bson;

namespace Dork.Service.Default.Impl
{
    public class EntityService<T> : IEntityService<T> where T : IEntity
    {
        public readonly IRepository<T> Repo;
        
        public EntityService(IRepository<T> repo)
        {
            Repo = repo;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Repo.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await Repo.GetByIdAsync(id);
        }

        public async Task<long> CreateElementAsync(T entity)
        {
            return await Repo.UpdateAsync(entity);
        }

        public async Task<long> UpdateElementAsync(T entity)
        {
            return await Repo.UpdateAsync(entity);
        }

        public async Task<long> DeleteElementAsync(string id)
        {
            return await Repo.DeleteAsync(id);
        }
    }
}
