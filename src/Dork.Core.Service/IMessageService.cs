using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Domain;

namespace Dork.Core.Service
{
    public interface IMessageService
    {
        IEnumerable<ActMessage> GetRecentActsByUserProfile(User userProfile, Page paging);
        ContentBase GetActContentByContentId(string id);
        ReactMessage PersistReactMessage(ReactMessage message);
        IDictionary<User, IEnumerable<ActMessage>> GetActUpdatesFromFriendList(User user);
        IEnumerable<MessageBase> GetMessageHistoryFromUsers(User user1, User user2);
    }
}
