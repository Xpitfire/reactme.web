using Dork.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Dork.Core.Domain;
using System.Threading.Tasks;

namespace Dork.Service.Default.Impl
{
    class AuthService : IAuthService
    {
        public Task<AuthState> AuthViaFacebookAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<AuthState> AuthViaGooglePlusAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<AuthState> LoginAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<AuthState> RegisterAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<AuthState> ResetPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
