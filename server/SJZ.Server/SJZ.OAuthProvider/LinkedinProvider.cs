using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.OAuthProvider
{
    public class LinkedInProvider : AuthProviderBase
    {
		public LinkedInProvider(OAuth2Config config)
			: base(config)
		{
		}

		protected override string AccessTokenUrl => "https://www.linkedin.com/uas/oauth2/accessToken";
		protected override string UserInfoUrl => "https://api.linkedin.com/v2/me";
		protected override int GetUserInfoMethod => 0;

		protected override UserInfo GetUserInfo(JObject obj)
		{
			return new UserInfo
			{
				Id = (string)obj["id"],
				DisplayName = $"{(string)obj["localizedFirstName"]} {(string)obj["localizedLastName"]}",
			};
		}
	}
}
