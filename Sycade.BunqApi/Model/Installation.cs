using System.Linq;

namespace Sycade.BunqApi.Model
{
    public class CreateInstallationResponse
    {
        public Id Id { get; }
        public Token Token { get; }
        public ServerPublicKey ServerPublicKey { get; }

        public CreateInstallationResponse(BunqEntity[] entities)
        {
            Id = entities.OfType<Id>().First();
            Token = entities.OfType<Token>().First();
            ServerPublicKey = entities.OfType<ServerPublicKey>().First();
        }
    }
}
