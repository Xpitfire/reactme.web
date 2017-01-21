using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dork.Core.Service
{
    public interface IUserService
    {
        AuthState ChangePassword(string passwordHash);
    }
}
