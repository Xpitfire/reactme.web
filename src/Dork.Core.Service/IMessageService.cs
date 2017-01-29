using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Domain;

namespace Dork.Core.Service
{
    public interface IMessageService
    {
        Task<IEnumerable<ActMessage>> GetRecentActsByUserProfileAsync(User userProfile, Page paging);
        Task<Content> GetActContentByContentIdAsync(string id);
        Task<ReactMessage> PersistReactMessageAsync(ReactMessage message);
        Task<IDictionary<User, IEnumerable<ActMessage>>> GetActUpdatesFromFriendListAsync(User user);
        Task<IEnumerable<MessageBase>> GetMessageHistoryFromUsersAsync(User user1, User user2);
    }
}
