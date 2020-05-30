using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Grpc.Core;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
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
    public class AccountController : ControllerBase
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IEventService _events;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IEventService events)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _events = events;
        }

        [HttpGet("Signout")]
        [HttpPost("Signout")]
        public async Task<IActionResult> Signout([FromQuery] string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            return new RedirectResult(logout.PostLogoutRedirectUri);
        }
    }
}
