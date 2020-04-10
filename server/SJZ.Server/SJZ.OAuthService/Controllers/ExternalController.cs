using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SJZ.OAuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExternalController : ControllerBase
    {
        public ExternalController()
        {
        }

        [HttpGet("{provider}")]
        public IActionResult Challenge(string provider, [FromQuery]string returnUrl)
        {
            try
            {
                // start challenge and roundtrip the return URL and scheme 
                var props = new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(Callback)),
                    Items =
                    {
                        { "returnUrl", returnUrl },
                        { "scheme", provider },
                    }
                };

                return Challenge(props, provider);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Callback()
        {
            // read external identity from the temporary cookie
            var result = await HttpContext.AuthenticateAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            var extPrincipal = result.Principal;
            var expProperties = result.Properties;
            var claims = extPrincipal.Claims.ToList();

            var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            if (userIdClaim == null)
            {
                userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            }
            if (userIdClaim == null)
            {
                throw new Exception("Unknown userid");
            }

            claims.Remove(userIdClaim);
            var provider = expProperties.Items["scheme"];
            var userId = userIdClaim.Value;

            var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";
            return Redirect(returnUrl);
        }

        [HttpGet("signout")]
        [HttpPost("signout")]
        public async Task<IActionResult> Signout()
        {
            var result = await HttpContext.AuthenticateAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
            var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";
            return SignOut(new AuthenticationProperties { RedirectUri = returnUrl },
               IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
        }
    }
}
