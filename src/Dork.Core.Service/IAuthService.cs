using Dork.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dork.Core.Service
{
    public interface IAuthService
    {
        Task<AuthState> RegisterAsync(User user);
        Task<AuthState> AuthViaFacebookAsync(User user);
        Task<AuthState> AuthViaGooglePlusAsync(User user);
        Task<AuthState> LoginAsync(User user);
        Task<AuthState> ResetPasswordAsync(string email);
    }
}
