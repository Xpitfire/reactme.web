using System;
using System.Collections.Generic;
using System.Text;

namespace Dork.Core.Domain.DTO
{
    public class LoginCredentials
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
