using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJZ.UserProfileService.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ThirdPartyProvider { get; set; }
        public string ThirdPartyId { get; set; }
    }
}
