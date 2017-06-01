using Sycade.BunqApi.Model;
using Sycade.BunqApi.Model.Users;
using System.Linq;

namespace Sycade.BunqApi.Responses
{
    public class Session : BunqEntity
    {
        public Id Id { get; }
        public Token Token { get; }
        public User User { get; }
        
        internal Session(BunqEntity[] entities)
        {
            Id = entities.OfType<Id>().First();
            Token = entities.OfType<Token>().First();
            User = entities.OfType<User>().First();
        }
    }
}
