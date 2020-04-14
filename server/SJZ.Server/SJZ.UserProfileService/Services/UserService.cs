using Grpc.Core;
using Microsoft.Extensions.Logging;
using SJZ.UserProfile.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJZ.UserProfileService.Services
{
    public class UserService : UserSvc.UserSvcBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(
            IUserRepository userRepository,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public override async Task<UserResponse> GetOrCreate(UserRequest request, ServerCallContext context)
        {
            var user = await _userRepository.GetUserBySocialIdAsync(request.ThirdPartyProvider, request.ThirdPartyId);
            if (user == null)
            {
                user = await _userRepository.CreateUserAsync(new User(request.FirstName, request.LastName, request.Email), request.ThirdPartyProvider, request.ThirdPartyId);
            }
            return new UserResponse { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
        }
    }
}
