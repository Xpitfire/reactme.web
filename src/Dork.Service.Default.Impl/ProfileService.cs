using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dork.Core.Dal;
using Dork.Core.Domain;
using Dork.Core.Service;

namespace Dork.Service.Default.Impl
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Profile> _profileRepo;

        public ProfileService(IRepository<Profile> profile)
        {
            _profileRepo = profile;
        }

        public Task<IEnumerable<User>> GetFriendProfilesFromUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
