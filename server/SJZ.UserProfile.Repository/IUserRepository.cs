using System;
using System.Threading.Tasks;

namespace SJZ.UserProfile.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user, string socialType, string socialId);
        Task<User> GetUserBySocialIdAsync(string type, string id);
    }
}
