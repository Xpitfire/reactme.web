using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Login([FromBody] LoginCredentials loginCredentials)
        {
            try
            {
                var result = _signInManager.PasswordSignInAsync(
                    loginCredentials.Username, loginCredentials.PasswordHash, false, false).Result;
                SignInUserStringWithCookieProps(loginCredentials.Username);
                return result.Succeeded 
                    ? Ok(true) 
                    : StatusCode((int)HttpStatusCode.Conflict, "Login Failed");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Error");
            }
        }

        [HttpPost]
        [Route("logout")]
        [ProducesResponseType(typeof(void), 200)]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().RunSynchronously();
            return Ok();
        }
                

        private void SignInIdentityUserWithCookieProps(IdentityUser user)
        {
            //Cookie Props
            _signInManager.SignInAsync(user, false);
        }

        private void SignInUserStringWithCookieProps(string username)
        {
            var res = from u in _userManager.Users
                      where u.UserName == username
                      select u;

            var usr = res.FirstOrDefault();
            if (usr != null)
            {
                SignInIdentityUserWithCookieProps(usr);
            }
        }

    }
}
