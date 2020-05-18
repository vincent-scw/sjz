using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.OAuthProvider
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public UserInfo UserInfo { get; set; }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
    }
}
