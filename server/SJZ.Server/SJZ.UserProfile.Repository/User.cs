using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.UserProfile.Repository
{
    public class User
    {
        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; }   
        public User(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedDate = DateTimeOffset.UtcNow;
        }
    }
}
