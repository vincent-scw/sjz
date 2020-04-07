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
            new ApiResource("timeline.api")
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "spa",
                AllowedGrantTypes = GrantTypes.Implicit,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "timeline.api" }
            }
        };
    }
}
