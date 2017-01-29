using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Domain;

namespace Dork.Core.Service
{
    public interface IProfileService
    {
        Task<IEnumerable<User>> GetFriendProfilesFromUserAsync(User user);
    }
}
