using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Domain;

namespace Dork.Core.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(string id);

        Task<long> CreateElement(User entity);
        Task<long> UpdateElement(User entity);

        Task<long> DeleteElement(string id);

        AuthState ChangePassword(string passwordHash);
    }
}
