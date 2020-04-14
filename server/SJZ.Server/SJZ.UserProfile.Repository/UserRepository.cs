using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SJZ.UserProfile.Repository
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly IDriver _driver;
        public UserRepository(Neo4jConfig config)
        {
            _driver = GraphDatabase.Driver(config.Uri, AuthTokens.Basic(config.Username, config.Password));
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var session = _driver.AsyncSession();
            var u = await session.WriteTransactionAsync(async tx =>
            {
                var result = await tx.RunAsync(@"
MERGE (u:User {userId: $userid})
SET u.firstName = $firstName
SET u.lastName = $lastName
SET u.email = $email
SET u.createdDate = $createdDate
RETURN u
", new { userid = user.Id, firstName = user.FirstName, lastName = user.LastName, createdDate = new ZonedDateTime(user.CreatedDate) });
                var data = await result.SingleAsync();
                return data.As<dynamic>();
            });

            return user;
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}
