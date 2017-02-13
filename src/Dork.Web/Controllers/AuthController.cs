using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Domain;
using Dork.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.MongoDB;
using Dork.Core.Domain.DTO;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Dork.Web.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class AuthController : Controller
    {
        // private readonly IAuthService _authService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        

        public AuthController(/*IAuthService authService,*/ UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            //_authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
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


        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(loginCredentials.Username, loginCredentials.PasswordHash, false, false);
                await SignInUserStringWithCookieProps(loginCredentials.Username);
                if (result.Succeeded)
                    return Ok("Logged In");
                return StatusCode((int)HttpStatusCode.Conflict, "Login Failed");
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Error");
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("logged out");
        }
                

        private async Task SignInIdentityUserWithCookieProps(IdentityUser user)
        {
            //Cookie Props
            await _signInManager.SignInAsync(user, false);
        }

        private async Task SignInUserStringWithCookieProps(string username)
        {
            var res = from u in _userManager.Users
                      where u.UserName == username
                      select u;

            var usr = res.FirstOrDefault();
            if (usr != null)
            {
                await SignInIdentityUserWithCookieProps(usr);
            }
        }

    }
}
