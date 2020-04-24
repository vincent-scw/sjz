using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Grpc.Core;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SJZ.UserProfileService;

namespace SJZ.OAuthService.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class ExternalController : ControllerBase
    {
        private readonly UserSvc.UserSvcClient _userClient;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IEventService _events;

        public ExternalController(UserSvc.UserSvcClient userClient,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IEventService events)
        {
            _userClient = userClient;

            _interaction = interaction;
            _clientStore = clientStore;
            _events = events;
        }

        [HttpGet("{provider}")]
        public IActionResult Challenge(string provider)
        {
            try
            {
                var returnUrl = HttpContext.Request.QueryString.Value;
                returnUrl = returnUrl.Substring(11); // Remove ?ReturnUrl=
                // start challenge and roundtrip the return URL and scheme 
                var props = new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(Callback)),
                    Items =
                    {
                        { "returnUrl", System.Web.HttpUtility.UrlDecode(returnUrl) },
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
            var name = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            var email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);

            var user = await _userClient.GetOrCreateAsync(new UserRequest 
            { 
                Name = name?.Value,
                Email = email?.Value,
                ThirdPartyProvider = provider, 
                ThirdPartyId = userIdClaim.Value
            });

            // this allows us to collect any additonal claims or properties
            // for the specific prtotocols used and store them in the local auth cookie.
            // this is typically used to store data needed for signout from those protocols.
            var additionalLocalClaims = new List<Claim>();
            var localSignInProps = new AuthenticationProperties();
            ProcessLoginCallbackForOidc(result, additionalLocalClaims, localSignInProps);

            // issue authentication cookie for user
            await HttpContext.SignInAsync(
                user.Id, user.Name, provider, localSignInProps, additionalLocalClaims.ToArray()
            );

            await _events.RaiseAsync(new UserLoginSuccessEvent(provider, userIdClaim.Value, user.Id, user.Name));
            await HttpContext.SignOutAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);

            var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";

            return Redirect(returnUrl);
        }

        private void ProcessLoginCallbackForOidc(AuthenticateResult externalResult, List<Claim> localClaims, AuthenticationProperties localSignInProps)
        {
            // if the external system sent a session id claim, copy it over
            // so we can use it for single sign-out
            var sid = externalResult.Principal.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
            {
                localClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
            }

            // if the external provider issued an id_token, we'll keep it for signout
            var id_token = externalResult.Properties.GetTokenValue("id_token");
            if (id_token != null)
            {
                localSignInProps.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = id_token } });
            }
        }
    }
}
