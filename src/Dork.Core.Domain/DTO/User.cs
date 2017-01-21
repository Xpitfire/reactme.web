using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dork.Core.Domain
{
    public class User : EntityBase
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Profile Profile { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;
            var user = obj as User;
            if (user == null) return false;
            return Username == user.Username;
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return Username?.GetHashCode() ?? 0;
            }
        }
    }
}
