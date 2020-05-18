using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.OAuthProvider
{
    public class OAuth2Config
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
    }
}
