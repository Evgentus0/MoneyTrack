using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoneyTrack.Web.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class IsAliveController : MoneyTrackController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new 
            {
                IsAlive = true,
                ServerTime = DateTime.Now,
            });
        }
    }
}
