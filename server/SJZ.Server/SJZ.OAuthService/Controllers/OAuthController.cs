using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SJZ.OAuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OAuthController : ControllerBase
    {
        [HttpGet("links")]
        public ActionResult GetLinks()
        {
            return Ok(new
            {
                Linkedin = "https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id=81y34iwbqrnk79&redirect_uri=http%3A%2F%2Flocalhost%3A55227%2Foauth%2Flinkedin&scope=r_liteprofile%20r_emailaddress&state=anything"
            });
        }
    }
}
