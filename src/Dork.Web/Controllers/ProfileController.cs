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
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IEntityService<User> _userService;

        public ProfileController(
            IEntityService<User> userService, 
            IProfileService profileService)
        {
            _profileService = profileService;
        }
        
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _userService.GetAllAsync();
            return Ok(data);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User value)
        {
            var data = await _userService.CreateElementAsync(value);
            return Ok("Element created");
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]User value)
        {
            var linesWritten = await _userService.UpdateElementAsync(value);
            if (linesWritten == 1) return Ok("Element Updated");
            return StatusCode(409, "Couldn't update Element");

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteElementAsync(id);
            return Ok("Element deleted");
        }

    }
}
