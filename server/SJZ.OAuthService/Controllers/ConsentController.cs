using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJZ.OAuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsentController : ControllerBase
    {
        private readonly IIdentityServerInteractionService _interaction;
        public ConsentController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        [HttpGet]
        public async Task<IActionResult> Consent([FromQuery] string returnUrl)
        {
            // Bypass consent, just redirect to next 
            var grantedConsent = new ConsentResponse
            {
                RememberConsent = true,
                ScopesConsented = new List<string> {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "timelineapi",
                    "imageapi",
                    "ups"}
            };

            var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
            await _interaction.GrantConsentAsync(request, grantedConsent);

            return Redirect(returnUrl);
        }
    }
}
