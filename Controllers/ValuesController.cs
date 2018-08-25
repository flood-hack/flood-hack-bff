using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flood_hackathon.Models;
using Microsoft.AspNetCore.Mvc;

namespace flood_hackathon.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {

        }

        // GET api/values
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] QueryParams query)
        {

        }

        [HttpPost]
        public void AddTool([FromBody] string value)
        {

        }

        [HttpPatch]
        [Route("{id}")]
        public void UpdateTool(int id, [FromBody] string value)
        {

        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {

        }
    }
}
