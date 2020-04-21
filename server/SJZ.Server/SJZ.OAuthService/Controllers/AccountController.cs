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
    public class AccountController : ControllerBase
    {
        private readonly UserSvc.UserSvcClient _userClient;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IEventService _events;

        public AccountController(UserSvc.UserSvcClient userClient,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IEventService events)
        {
            _userClient = userClient;

            _interaction = interaction;
            _clientStore = clientStore;
            _events = events;
        }

        [HttpGet("Signout")]
        [HttpPost("Signout")]
        public async Task<IActionResult> Signout()
        {
            var result = await HttpContext.AuthenticateAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
            var returnUrl = result?.Properties?.Items["returnUrl"] ?? "~/";

            return SignOut(new AuthenticationProperties { RedirectUri = returnUrl },
               IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
        }
    }
}
