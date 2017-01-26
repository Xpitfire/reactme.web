using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dork.Core.Domain
{
    public class Profile : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ContentBase Image { get; set; }
        public IList<User> Friends { get; set; }
        public IList<InvitationStatus> InvitationOptions { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }

    public enum InvitationStatus
    {
        NativeAppUser,
        InvitedByEmail,
        InvitedBySms,
        InvitedByWhatsApp,
        InvitedByFacebook,
        InvitedByGooglePlus,
    }
}
