using System.Collections.Generic;
using System.Linq;
using Dork.Core.Domain;
using Dork.Core.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dork.Web.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class UserController : Controller
    {
        private readonly IEntityService<User> _userEntityService;
        private readonly IProfileService _profileService;

        public UserController(IEntityService<User> userEntityService, 
            IEntityService<Profile> profileEntityService,
            IProfileService profileService)
        {
            _userEntityService = userEntityService;
            _profileService = profileService;
        }

        /// <summary>
        /// GET user by ID:
        /// - api/[controller]/user/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/{id}")]
        [ProducesResponseType(typeof(User), 200)]
        public IActionResult GetUser(string id)
        {
            // TODO: Unable to cast object of type 'MongoDB.Bson.BsonString' to type 'System.String'
            var user = _userEntityService.GetByIdAsync(id).Result;
            return Ok(user);
        }


        [HttpPost]
        [Route("friends")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public IActionResult GetUserFriends([FromBody] User user)
        {
            var data = _profileService.GetFriendProfilesFromUserAsync(user).Result;
            return Ok(data);
        }

        [HttpPost]
        [Route("user/create")]
        [ProducesResponseType(typeof(long), 200)]
        public IActionResult CreateNewUser([FromBody] User user)
        {
            var id = _userEntityService.CreateElementAsync(user).Result;
            return Ok(id);
        }
        
        [HttpPut]
        [Route("user/update")]
        [ProducesResponseType(typeof(long), 200)]
        [ProducesResponseType(typeof(void), 400)]
        public IActionResult UpdateUser([FromBody] User value)
        {
            var linesWritten = _userEntityService.UpdateElementAsync(value).Result;
            return linesWritten == 1
                ? Ok(linesWritten)
                : StatusCode(409, "Couldn't update Element");
        }
        
        [HttpDelete]
        [Route("user/delete/{id}")]
        [ProducesResponseType(typeof(long), 200)]
        public IActionResult Delete(string id)
        {
            var resId = _userEntityService.DeleteElementAsync(id).Result;
            return Ok(resId);
        }
    }
}
