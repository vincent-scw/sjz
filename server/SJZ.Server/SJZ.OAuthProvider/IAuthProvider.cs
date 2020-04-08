using System;
using System.Threading.Tasks;

namespace SJZ.OAuthProvider
{
    public interface IAuthProvider
    {
        Task<AuthResponse> GetAuthResponseAsync(string code);
    }
}
