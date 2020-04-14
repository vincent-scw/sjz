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

        public async Task<User> CreateUserAsync(User user, string socialType, string socialId)
        {
            var session = _driver.AsyncSession();
            try
            {
                var u = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(@"
CREATE (u:User {userId: $userid, firstName: $firstName, lastName: $lastName, email: $email, createdDate: $createdDate})->[hasSocial]->(s:Social: {type: socialType, id: $socialId})
RETURN u", 
                    new 
                    { 
                        userid = user.Id, 
                        firstName = user.FirstName, 
                        lastName = user.LastName, 
                        createdDate = new ZonedDateTime(user.CreatedDate), 
                        socialType, 
                        socialId 
                    });
                    var data = await result.SingleAsync();
                    return data.As<dynamic>();
                });

                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<User> GetUserBySocialIdAsync(string type, string id)
        {
            var session = _driver.AsyncSession();
            try
            {
                var u = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(@"
MATCH(u:User)-[hasSocial]->(s:Social {type:$type, id:$id}) 
RETURN u", new { type, id });
                    var data = await result.SingleAsync();
                    return data.As<dynamic>();
                });

                return u;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}
