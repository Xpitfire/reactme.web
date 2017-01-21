using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Dal;
using Dork.Core.Domain;
using Dork.Core.Service;
using Dork.Dal.Mongo.Impl;

namespace Dork.Service.Default.Impl
{
    public class EntityService<T> : IEntityService<T> where T : IEntity
    {
        public readonly IRepository<T> _repo;

        // TODO: Inject Repo
        public EntityService()
        {
            _repo = new Repository<T>("mongodb://localhost:27017/local");
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<long> CreateElement(T entity)
        {
            return await _repo.UpdateAsync(entity);
        }

        public async Task<long> UpdateElement(T entity)
        {
            return await _repo.UpdateAsync(entity);
        }

        public async Task<long> DeleteElement(string id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
