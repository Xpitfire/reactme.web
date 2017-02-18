using Dork.Core.Domain;
using Dork.Core.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dork.Web.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IEntityService<User> _userEntityService;
        private readonly IEntityService<Profile> _profilEntityService;

        public ProfileController(
            IEntityService<User> userEntityService,
            IEntityService<Profile> profilEntityService,
            IProfileService profileService)
        {
            _profileService = profileService;
            _userEntityService = userEntityService;
            _profilEntityService = profilEntityService;
        }
        
        [HttpGet]
        [Route("profile/{id}")]
        [ProducesResponseType(typeof(Profile), 200)]
        public IActionResult GetProfile(string id)
        {
            var data = _userEntityService.GetByIdAsync(id).Result;
            return Ok(data);
        }

        [HttpPost]
        [Route("profile/create")]
        [ProducesResponseType(typeof(long), 200)]
        public IActionResult CreateNewProfile([FromBody] Profile profile)
        {
            var id = _profilEntityService.CreateElementAsync(profile).Result;
            return Ok(id);
        }

    }
}
