using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.UserProfile.Repository
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedDate { get; set; }   

        public User()
        {
            Id = shortid.ShortId.Generate(true, false, 12);
            CreatedDate = DateTimeOffset.UtcNow;
        }
    }
}
