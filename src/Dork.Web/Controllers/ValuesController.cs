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
        public IActionResult Get()
        {
            var data = _service.GetAll();
            return Ok(data.Result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_service.GetById(id).Result);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]User value)
        {
            var data = _service.CreateElement(value).Result;
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody]User value)
        {
            var data = _service.UpdateElement(value).Result;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
