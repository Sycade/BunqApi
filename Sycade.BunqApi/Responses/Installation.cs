using Sycade.BunqApi.Model;
using System.Linq;

namespace Sycade.BunqApi.Responses
{
    public class Installation
    {
        public Id Id { get; }
        public Token Token { get; }
        public ServerPublicKey ServerPublicKey { get; }

        public Installation(BunqEntity[] entities)
        {
            Id = entities.OfType<Id>().First();
            Token = entities.OfType<Token>().First();
            ServerPublicKey = entities.OfType<ServerPublicKey>().First();
        }
    }
}
