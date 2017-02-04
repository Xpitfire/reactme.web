using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Domain;
using Dork.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace Dork.Web.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(new User
            {
                Id = "1",
                Username = "xpitfire", 
                Email = "test@user.com",
                PasswordHash = "xxdkdjflckj3i02+=",
                ProfileId = "xpitfireprofile",
                Status = UserStatus.Active
            });
        }

    }
}
