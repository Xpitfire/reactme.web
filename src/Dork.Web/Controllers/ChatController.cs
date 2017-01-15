using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Domain.Message;
using Microsoft.AspNetCore.Mvc;

namespace Dork.Web.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : Controller
    {

        [HttpGet("[action]")]
        public ActorMessage Get([Required] long id)
        {
            return new ActorMessage {Id = 1};
        }

        [HttpPost]
        [ProducesResponseType(typeof(ActorMessage), 201)]
        [ProducesResponseType(typeof(ActorMessage), 400)]
        public IActionResult Create([FromBody, Required] ActorMessage item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            Debug.WriteLine(item.Id);
            return CreatedAtRoute("GetActorMessage", new { id = item.Id }, item);
        }

    }
}
