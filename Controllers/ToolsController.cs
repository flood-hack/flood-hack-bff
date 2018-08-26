using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using flood_hackathon.Models;
using flood_hackathon.Models.Requests;
using flood_hackathon.Models.Responses;
using flood_hackathon.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace flood_hackathon.Controllers
{
    [Route("api/tools")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private ToolsService _toolsService;
        private SearchIndexSettings _settings;
        public ToolsController(ToolsService toolsService, IOptions<SearchIndexSettings> settings)
        {
            _toolsService = toolsService;
            _settings = settings.Value;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTool(string id, CancellationToken cancellationToken)
        {
            var tool = await _toolsService.GetTool(id, cancellationToken);
            return new ObjectResult(tool);
        }

        [HttpGet]
        public async Task<IActionResult> GetTools([FromQuery] QueryParams query, CancellationToken cancellationToken)
        {
            var tools = await _toolsService.GetTools(query, cancellationToken);
            return new ObjectResult(new ToolsResponse()
            {
                Tools = tools
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddTool([FromBody] AddEditToolRequest request, CancellationToken cancellationToken)
        {
            await _toolsService.AddTool(request, cancellationToken);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTool(string id, [FromBody] AddEditToolRequest request, CancellationToken cancellationToken)
        {
            await _toolsService.UpdateTool(id, request, cancellationToken);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            await _toolsService.DeleteTool(id, cancellationToken);
            return NoContent();
        }

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> GetInfo(CancellationToken cancellationToken)
        {
            await _toolsService.DeleteTool("test", cancellationToken);
            return Ok();
        }

        [HttpGet]
        [Route("env")]
        public async Task<IActionResult> GetEnv(CancellationToken cancellationToken)
        {
            return new ObjectResult(_settings);
        }
    }
}
