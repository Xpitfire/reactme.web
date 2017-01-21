using Dork.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dork.Core.Service
{
    public interface IAuthService
    {
        AuthState Register(User user);
        AuthState AuthViaFacebook(User user);
        AuthState AuthViaGooglePlus(User user);
        AuthState Login(User user);
        AuthState ResetPassword(string email);
    }
}
