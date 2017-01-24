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
    public class ValuesController : Controller
    {
        private readonly IEntityService<User> _service;

        public ValuesController(IEntityService<User> service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _service.GetAll();
            return Ok(data);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _service.GetById(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User value)
        {
            var data = await _service.CreateElement(value);
            return Ok("Element created");
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]User value)
        {
            var linesWritten = await _service.UpdateElement(value);
            if (linesWritten == 1) return Ok("Element Updated");
            return StatusCode(409, "Couldn't update Element");

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteElement(id);
            return Ok("Element deleted");
        }
    }
}
