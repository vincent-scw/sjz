using System;
using System.Threading.Tasks;

namespace SJZ.UserProfile.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
    }
}
