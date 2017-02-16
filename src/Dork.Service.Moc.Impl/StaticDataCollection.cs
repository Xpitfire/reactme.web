using Dork.Core.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Dork.Service.Moc.Impl
{
    public static class StaticDataCollection
    {
        public static readonly IDictionary<string, User> Users;
        public static readonly IDictionary<string, Profile> Profiles;
        public static readonly IDictionary<string, ContactInfo> ContactInfos;
        public static readonly IDictionary<string, Content> Contents;
        public static readonly IDictionary<string, Media> Medias;
        public static readonly IDictionary<string, MessageBase> Messages;
        public static readonly IDictionary<string, Note> Notes;
        public static readonly IDictionary<User, IEnumerable<ActMessage>> ActUpdatesFromFriendList;
        
        static StaticDataCollection()
        {
            Users = new ConcurrentDictionary<string, User>();
            Profiles = new ConcurrentDictionary<string, Profile>();
            ContactInfos = new ConcurrentDictionary<string, ContactInfo>();
            Contents = new ConcurrentDictionary<string, Content>();
            Medias = new ConcurrentDictionary<string, Media>();
            Messages = new ConcurrentDictionary<string, MessageBase>();
            Notes = new ConcurrentDictionary<string, Note>();
            ActUpdatesFromFriendList = new ConcurrentDictionary<User, IEnumerable<ActMessage>>();

            DefineFlowData();
            DefineXpitfireData();
            DefineGrizzlorrData();

            DefineXpitfireActMessage();
        }

        #region grizzlorr Definition
        private static void DefineGrizzlorrData()
        {
            var contactInfoId = "unknown1";
            var profileId = "unknown1";
            var userId = "unknown1";
            ContactInfos[contactInfoId] = new ContactInfo
            {
                PhoneNumbers = { "+436763456789" }
            };
            Profiles[profileId] = new Profile
            {
                Id = profileId,
                InvitationOptions = InvitationStatus.InvitedBySms
            };
            Users[userId] = new User
            {
                Id = userId,
                Status = UserStatus.Inactive,
                ProfileId = profileId
            };
        }
        #endregion
        
        #region flow Definition
        private static void DefineFlowData()
        {
            var mediaId = "flow1";
            var thumbnailId = "flow2";
            var contentId = "flow";
            var contactInfoId = "flow";
            var profileId = "flow";
            var userId = "flow";
            Medias[mediaId] = new Media
            {
                Id = mediaId,
                Data = new byte[] { 00, 01, 02, 03, 04, 05 }
            };
            Medias[thumbnailId] = new Media
            {
                Id = thumbnailId,
                Data = new byte[] { 00, 01, 02, 03, 04, 05 }
            };
            Contents[contentId] = new Content
            {
                Id = contentId,
                MediaId = mediaId,
                MimeType = MimeType.ImageJpeg,
                Size = 3,
                ThumbnailId = thumbnailId
            };
            ContactInfos[contactInfoId] = new ContactInfo
            {
                Emails = { "flow@hotmail.com" },
                PhoneNumbers = { "+436762345678" }
            };
            Profiles[profileId] = new Profile
            {
                Id = profileId,
                FirstName = "Florian",
                LastName = "Wurm",
                InvitationOptions = InvitationStatus.NativeAppUser,
                FriendIds = { "xpitfire", "unknown1" },
                ContactInfo = ContactInfos[contactInfoId],
                Image = Contents[contentId]
            };
            Users[userId] = new User
            {
                Id = userId,
                Username = "flow",
                Email = "flow@gmail.com",
                PasswordHash = "flow",
                Status = UserStatus.Active,
                ProfileId = profileId
            };
        }
        #endregion

        #region xpitfire Definition
        private static void DefineXpitfireData()
        {
            var mediaId = "xpitfire1";
            var thumbnailId = "xpitfire2";
            var contentId = "xpitfire";
            var contactInfoId = "xpitfire";
            var profileId = "xpitfire";
            var userId = "xpitfire";
            Medias[mediaId] = new Media
            {
                Id = mediaId,
                Data = new byte[] { 00, 01, 02, 03, 04, 05 }
            };
            Medias[thumbnailId] = new Media
            {
                Id = thumbnailId,
                Data = new byte[] { 00, 01, 02, 03, 04, 05 }
            };
            Contents[contentId] = new Content
            {
                Id = contentId,
                MediaId = mediaId,
                MimeType = MimeType.ImageJpeg,
                Size = 3,
                ThumbnailId = thumbnailId
            };
            ContactInfos[contactInfoId] = new ContactInfo
            {
                FacebookAccount = "facebook001",
                GooglePlusAccount = "googleplus001",
                Emails = { "xpitfire@hotmail.com", "xpitfire@gmail.com" },
                PhoneNumbers = { "+436761234567", "+17741234567" }
            };
            Profiles[profileId] = new Profile
            {
                Id = profileId,
                FirstName = "Marius",
                LastName = "Dinu",
                InvitationOptions = InvitationStatus.NativeAppUser,
                FriendIds = { "flow" },
                ContactInfo = ContactInfos[contactInfoId],
                Image = Contents[contentId]
            };
            Users[userId] = new User
            {
                Id = userId,
                Username = "xpitfire",
                Email = "xpitfire@gmail.com",
                PasswordHash = "xpitfire",
                Status = UserStatus.Active,
                ProfileId = profileId
            };
        }
        #endregion

        #region xpitfire Message Definition
        private static void DefineXpitfireActMessage()
        {
            var actMessageId = "act:xpitfire@flow";
            var reactMessageId = "react:xpitfire@flow";
            var senderId = "xpitfire";
            var receiverId = "flow";
            var actCondentId = "xpitfire@flow";
            var reactContentId = "flow@xpitfire";
            var noteId = "xpitfire@flow";
            var actThumbnailId = "xpitfire@flow";
            var reactThumbnailId = "flow@xpitfire";
            var actMediaId = "xpitfire@flow";
            var reactMediaId = "flow@xpitfire";

            Medias[actMediaId] = new Media
            {
                Id = actMediaId,
                Data = new byte[] { 0, 1, 2, 3, 4 }
            };

            Medias[reactMediaId] = new Media
            {
                Id = reactMediaId,
                Data = new byte[] { 0, 1, 2, 3, 4 }
            };

            Contents[actCondentId] = new Content
            {
                Id = actCondentId,
                MimeType = MimeType.ImageGif,
                Size = 5,
                ThumbnailId = actThumbnailId,
                MediaId = actMediaId
            };

            Contents[reactContentId] = new Content
            {
                Id = actCondentId,
                MimeType = MimeType.VideoMp4,
                Size = 5,
                ThumbnailId = reactThumbnailId,
                MediaId = reactMediaId
            };

            Notes[noteId] = new Note
            {
                Id = noteId,
                Text = "super awesome",
                Rank =  Rank.Awesome
            };

            Messages[actMessageId] = new ActMessage
            {
                Id = actMessageId,
                SenderId = senderId,
                DateCreated = DateTime.Now.AddMinutes(-44).Ticks,
                DateReceived = DateTime.Now.AddMinutes(-34).Ticks,
                DateViewed = DateTime.Now.AddMinutes(-24).Ticks,
                Description = "funny clips",
                ReceiverIds = {receiverId},
                Type = ActType.PrivateAct,
                Content = Contents[actCondentId]
            };

            Messages[reactMessageId] = new ReactMessage
            {
                Id = reactMessageId,
                SenderId = receiverId,
                ActMessage = (ActMessage)Messages[actCondentId],
                DateCreated = DateTime.Now.Ticks,
                DateReceived = DateTime.Now.AddMilliseconds(10).Ticks,
                Comment = Notes[noteId],
                Tags = { "fun", "stupid", "2016" },
                Content = Contents[actCondentId]
            };

            ActUpdatesFromFriendList[Users[senderId]] =
                new List<ActMessage>() { (ActMessage)Messages[actMessageId] };
        }
        #endregion


    }
}
