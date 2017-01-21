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
    public class UserService : IUserService
    {

        public readonly IRepository<User> _repo;

        // TODO: Inject Repo
        public UserService()
        {
            _repo = new Repository<User>("mongodb://localhost:27017/DORK");
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<User> GetById(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<long> CreateElement(User entity)
        {
            return await _repo.UpdateAsync(entity);
        }

        public async Task<long> UpdateElement(User entity)
        {
            return await _repo.UpdateAsync(entity);
        }

        public async Task<long> DeleteElement(string id)
        {
            return await _repo.DeleteAsync(id);
        }

        public AuthState ChangePassword(string passwordHash)
        {
            throw new NotImplementedException();
        }
    }
}
