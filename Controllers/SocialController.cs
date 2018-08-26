using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using flood_hackathon.Clients;
using flood_hackathon.Models;
using flood_hackathon.Models.Requests;
using flood_hackathon.Models.Responses;
using flood_hackathon.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace flood_hackathon.Controllers
{
    [Route("api/social")]
    [ApiController]
    public class SocialController : ControllerBase
    {
        private SocialClient _socialClient;
        
        public SocialController(SocialClient socialClient)
        {
            _socialClient = socialClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetSocial()
        {
            return await _socialClient.GetSocial(Request.QueryString);
        }

        [HttpGet]
        [Route("geo")]
        public async Task<IActionResult> GetGeo()
        {
            return await _socialClient.GetGeo(Request.QueryString);
        }

        [HttpGet]
        [Route("ping")]
        public async Task<IActionResult> Ping(CancellationToken cancellationToken)
        {
            return Content("HEY!");
        }
    }
}
