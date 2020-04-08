
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SJZ.OAuthProvider
{
	public abstract class AuthProviderBase : IAuthProvider
	{
		private OAuth2Config _config;

		public AuthProviderBase(OAuth2Config config)
		{
			_config = config;
		}

		public async Task<AuthResponse> GetAuthResponseAsync(string code)
		{
			var accessToken = await GetAccessTokenAsync(code);
			if (string.IsNullOrEmpty(accessToken))
				throw new InvalidOperationException("Auth code invalid.");

			var userInfo = GetUserInfo(await GetUserInfoFromProviderAsync(accessToken));
			return new AuthResponse { AccessToken = accessToken, UserInfo = userInfo };
		}

		protected abstract string AccessTokenUrl { get; }
		protected abstract string UserInfoUrl { get; }
		/// <summary>
		/// // 0 header, 1 querystring
		/// </summary>
		protected abstract int GetUserInfoMethod { get; }
		protected abstract UserInfo GetUserInfo(JObject obj);

		protected virtual async Task<string> GetAccessTokenAsync(string code)
		{
			var tokenUrl = AccessTokenUrl;
			var client = new HttpClient();
			var formContent = new FormUrlEncodedContent(
				new Dictionary<string, string>
				{
					{"grant_type", "authorization_code"},
					{"client_id", _config.ClientId},
					{"client_secret", _config.ClientSecret},
					{"code", code},
					{"redirect_uri", _config.RedirectUrl}
				});

			var response = await client.PostAsync(tokenUrl, formContent);
			var contentStr = await response.Content.ReadAsStringAsync();
			return (string)(JObject.Parse(contentStr)["access_token"]);
		}

		private async Task<JObject> GetUserInfoFromProviderAsync(string accessToken)
		{
			var userInfoUrl = UserInfoUrl;
			var client = new HttpClient();
			var message = new HttpRequestMessage(HttpMethod.Get, new Uri(userInfoUrl));
			if (GetUserInfoMethod == 0)
			{
				message.Headers.Authorization =
					new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
			}
			else
			{
				message.RequestUri = new Uri($"{userInfoUrl}?access_token={accessToken}");
			}

			var response = await client.SendAsync(message);
			var contentStr = await response.Content.ReadAsStringAsync();
			return JObject.Parse(contentStr);
		}
	}
}
