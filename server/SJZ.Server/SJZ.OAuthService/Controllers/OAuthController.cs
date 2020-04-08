using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SJZ.OAuthProvider;

namespace SJZ.OAuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OAuthController : ControllerBase
    {
        private readonly OAuth2ProviderFactory _factory;
        public OAuthController(OAuth2ProviderFactory providerFactory)
        {
            _factory = providerFactory;
        }

        [HttpGet("api-links")]
        public ActionResult GetLinks()
        {
            var linkedinConfig = _factory.OpenIdAuthConfigs.LinkedInConfig;
            return Ok(new
            {
                LinkedIn = "https://www.linkedin.com/oauth/v2/authorization?" +
                    $"response_type=code&client_id={linkedinConfig.ClientId}" + 
                    $"&redirect_uri={System.Net.WebUtility.UrlEncode(linkedinConfig.RedirectUrl)}" + 
                    "&scope=r_liteprofile%20r_emailaddress" + 
                    "&state=anything"
            });
        }

        [HttpGet("{type}")]
        public async Task<ActionResult> Callback(string type, [FromQuery]string code, [FromQuery]string state)
        {
            try
            {
                //todo: Verify state
                var oauthProvider = _factory.GetProvider(type);
                var response = await oauthProvider.GetAuthResponseAsync(code);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
