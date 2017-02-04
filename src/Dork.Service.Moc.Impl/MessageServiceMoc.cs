using Dork.Core.Service;
using System.Collections.Generic;
using Dork.Core.Domain;
using System.Threading.Tasks;
using System;

namespace Dork.Service.Moc.Impl
{
    public class MessageServiceMoc : IMessageService
    {

        public Task<Content> GetActContentByContentIdAsync(string id)
        {
            return Task.Run(() => StaticDataCollection.Contents[id]);
        }

        public Task<IDictionary<User, IEnumerable<ActMessage>>> GetActUpdatesFromFriendListAsync(User user)
        {
            return Task.Run(() => StaticDataCollection.ActUpdatesFromFriendList);
        }

        public Task<IEnumerable<MessageBase>> GetMessageHistoryFromUsersAsync(User user1, User user2)
        {
            return Task.Run(() =>
            {
                IEnumerable<MessageBase> messages = new List<MessageBase> { StaticDataCollection.Messages["react:xpitfire@flow"] };
                return messages;
            });
        }

        public Task<IEnumerable<ActMessage>> GetRecentActsByUserProfileAsync(User userProfile, Page paging)
        {
            return Task.Run(() => {
                IEnumerable<ActMessage> res = new List<ActMessage> { (ActMessage)StaticDataCollection.Messages["act:xpitfire@flow"] };
                return res;
            });
        }

        public Task<ActMessage> PersistActMessageAsync(ActMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<ReactMessage> PersistReactMessageAsync(ReactMessage message)
        {
            return Task.Run(() => (ReactMessage)StaticDataCollection.Messages["xpitfire"]);
        }
    }
}
