using Dork.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Dork.Core.Domain;
using System.Threading.Tasks;

namespace Dork.Service.Moc.Impl
{
    public class ProfileServiceMoc : IProfileService
    {
        public Task<IEnumerable<User>> GetFriendProfilesFromUserAsync(User user)
        {
            return Task.Run(() => {
                var coll = StaticDataCollection.Users.Values;
                var enumerator = coll.GetEnumerator();
                var list = new List<User>();
                while (enumerator.MoveNext())
                {
                    list.Add(enumerator.Current);
                }
                IEnumerable<User> res = list;
                return res;
            });
        }
    }
}
