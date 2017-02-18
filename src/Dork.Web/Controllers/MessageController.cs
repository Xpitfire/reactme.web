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
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        [Route("message/recent/act/{id}")]
        [ProducesResponseType(typeof(IEnumerable<ActMessage>), 200)]
        public IActionResult GetRecentActsByUserProfileAsync([FromBody] User user, [FromBody] Page page)
        {
            var messages = _messageService.GetRecentActsByUserProfileAsync(user, page).Result;
            return Ok(messages);
        }
    }
}
