using Dork.Core.Service;
using System;
using Dork.Core.Domain;
using System.Threading.Tasks;

namespace Dork.Service.Moc.Impl
{
    public class AuthServiceMoc : IAuthService
    {
        public Task<AuthState> AuthViaFacebookAsync(User user)
        {
            return Task.Run(() => AuthState.Successful);
        }

        public Task<AuthState> AuthViaGooglePlusAsync(User user)
        {
            return Task.Run(() => AuthState.Successful);
        }

        public Task<AuthState> LoginAsync(User user)
        {
            return Task.Run(() => AuthState.Successful);
        }

        public Task<AuthState> RegisterAsync(User user)
        {
            return Task.Run(() => AuthState.Successful);
        }

        public Task<AuthState> ResetPasswordAsync(string email)
        {
            return Task.Run(() => AuthState.Successful);
        }
    }
}
