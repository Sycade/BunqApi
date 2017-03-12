using Sycade.BunqApi.Model;
using System.Linq;

namespace Sycade.BunqApi.Responses
{
    public class Session : IBunqEntity
    {
        public Id Id { get; }
        public Token Token { get; }
        public User User { get; }

        internal Session(IBunqEntity[] entities)
        {
            Id = entities.OfType<Id>().First();
            Token = entities.OfType<Token>().First();
            User = entities.OfType<User>().First();
        }
    }
}
