using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.OAuthProvider
{
	public class OAuth2ProviderFactory
	{
		public OpenIdAuthConfigs OpenIdAuthConfigs { get; }

		public OAuth2ProviderFactory(OpenIdAuthConfigs options)
		{
			OpenIdAuthConfigs = options;
		}

		public IAuthProvider GetProvider(string type)
		{
			switch (type.ToLower())
			{
				case "linkedin":
					return new LinkedInProvider(OpenIdAuthConfigs.LinkedInConfig);
				//case AccountType.Microsoft:
				//	return new MicrosoftProvider(_openIdAuthorization.MicrosoftConfig);
				default:
					throw new NotSupportedException($"{type} is a not supported OAuth provider.");
			}
		}
	}
}
