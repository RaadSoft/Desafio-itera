using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Desatio_itera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        // GET api/<GrupoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GrupoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GrupoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GrupoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
