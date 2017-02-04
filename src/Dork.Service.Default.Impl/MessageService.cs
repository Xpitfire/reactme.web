using Dork.Core.Service;
using System;
using System.Linq;
using System.Collections.Generic;
using Dork.Core.Domain;
using System.Threading.Tasks;
using Dork.Core.Dal;

namespace Dork.Service.Default.Impl
{
    [Service]
    public class MessageService : IMessageService
    {

        private readonly IRepository<Content> _contentRepository;
        private readonly IRepository<Profile> _profileRepository;
        private readonly IRepository<ActMessage> _actMessageRepository;
        private readonly IRepository<ReactMessage> _reActMessageRepository;
        private readonly IRepository<User> _userRepository;

        public MessageService(IRepository<Content> contentRepository,
                              IRepository<Profile> profileRepository,
                              IRepository<ActMessage> actMessageRepository,
                              IRepository<ReactMessage> reActMessageRepository,
                              IRepository<User> userRepository)
        {
            _contentRepository = contentRepository;
            _profileRepository = profileRepository;
            _actMessageRepository = actMessageRepository;
            _reActMessageRepository = reActMessageRepository;
            _userRepository = userRepository;
        }

        public Task<Content> GetActContentByContentIdAsync(string id)
        {
            // TODO add Media byte[]
            return _contentRepository.GetByIdAsync(id);
        }

        public async Task<IDictionary<User, IEnumerable<ActMessage>>> GetActUpdatesFromFriendListAsync(User user)
        {
            var resultDictionary = new Dictionary<User, IEnumerable<ActMessage>>();
            var profile = await _profileRepository.GetByIdAsync(user.ProfileId);
            var actList = await _actMessageRepository.GetAllAsync(x => x.ReceiverIds.Contains(user.Id));

            foreach (var friendId in profile.FriendIds)
            {
                var acts = from act in actList
                           where act.SenderId == friendId && act.DateViewed == 0
                           select act;
                if (acts.Any())
                {
                    var friend = await _userRepository.GetByIdAsync(friendId);
                    resultDictionary.Add(friend, acts);
                }
            }
            return resultDictionary;
        }

        public async Task<IEnumerable<MessageBase>> GetMessageHistoryFromUsersAsync(User user1, User user2)
        {
            var result = new List<MessageBase>();
            result.AddRange(await _actMessageRepository.GetAllAsync(act => act.SenderId == user1.Id && act.ReceiverIds.Contains(user2.Id)));
            result.AddRange(await _actMessageRepository.GetAllAsync(act => act.SenderId == user2.Id && act.ReceiverIds.Contains(user1.Id)));
            result.AddRange(await _reActMessageRepository.GetAllAsync(react => react.SenderId == user1.Id && react.ActMessage.ReceiverIds.Contains(user2.Id)));
            result.AddRange(await _reActMessageRepository.GetAllAsync(react => react.SenderId == user2.Id && react.ActMessage.ReceiverIds.Contains(user1.Id)));
            return result;
        }

        public async Task<ActMessage> PersistActMessageAsync(ActMessage message)
        {
            var res = await _actMessageRepository.UpdateAsync(message);
            if (res != 0)
            {
                // TODO implement notification
                return message;
            }
            return null;
        }

        public async Task<ReactMessage> PersistReactMessageAsync(ReactMessage message)
        {
            var res = await _reActMessageRepository.UpdateAsync(message);
            if (res != 0)
            {
                // TODO implement notification
                return message;
            }
            return null;

        }

        public async Task<IEnumerable<ActMessage>> GetRecentActsByUserProfileAsync(User userProfile, Page paging)
        {
            var profile = await _profileRepository.GetByIdAsync(userProfile.Id);
            var actList = new List<ActMessage>();
            foreach (var friendId in profile.FriendIds)
            {
                var sinceTicks = DateTime.UtcNow.Ticks - paging.TimeSpan;
                var actListperFriend = await _actMessageRepository.GetAllAsync(act => act.SenderId == friendId && act.ReceiverIds.Contains(userProfile.Id) && act.DateCreated > sinceTicks);
                actList.AddRange(actListperFriend);
            }

            return actList;
        }
    }
}
