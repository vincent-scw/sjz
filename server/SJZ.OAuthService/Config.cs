using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJZ.OAuthService
{
    public class Config
    {
        public static IEnumerable<ApiResource> Apis => new List<ApiResource>
        {
            new ApiResource("timelineapi"),
            new ApiResource("ups")
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "clientApp",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "ups" }
            },
            new Client
            {
                ClientId = "spa",
                ClientName = "SPA Code Client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                AccessTokenType = AccessTokenType.Jwt,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "timelineapi",
                    "ups"
                },
                AllowedCorsOrigins = new List<string>
                {
                    "http://localhost:4200",
                    "https://www.timelines.top"
                },
                RequireClientSecret = false,
                RequirePkce = true,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = new List<string>
                {
                    "http://localhost:4200",
                    "http://localhost:4200/silent-renew.html",
                    "https://www.timelines.top",
                    "https://www.timelines.top/silent-renew.html"
                },
                PostLogoutRedirectUris = new List<string>
                {
                    "http://localhost:4200",
                    "http://localhost:4200/unauthorized",
                    "https://www.timelines.top",
                    "https://www.timelines.top/unauthorized"
                },
            }
        };

        public static List<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };
    }
}
