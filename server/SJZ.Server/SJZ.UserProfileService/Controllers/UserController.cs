using Microsoft.AspNetCore.Mvc;
using SJZ.UserProfile.Repository;
using SJZ.UserProfileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJZ.UserProfileService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository repository)
        {
            _userRepository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> GetOrCreateAsync([FromBody] UserModel request)
        {
            var user = await _userRepository.GetUserBySocialIdAsync(request.ThirdPartyProvider, request.ThirdPartyId);
            if (user == null)
            {
                user = await _userRepository.CreateUserAsync(new User
                {
                    Name = request.Name,
                    Email = request.Email
                }, request.ThirdPartyProvider, request.ThirdPartyId);
            }
            return Ok(new { user.Id, user.Name, user.Email });
        }
    }
}
