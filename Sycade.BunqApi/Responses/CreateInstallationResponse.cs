using System.Linq;

namespace Sycade.BunqApi.Model
{
    public class CreateInstallationResponse
    {
        public Id Id { get; }
        public Token Token { get; }
        public ServerPublicKey ServerPublicKey { get; }

        public CreateInstallationResponse(IBunqEntity[] responseObjects)
        {
            Id = responseObjects.OfType<Id>().First();
            Token = responseObjects.OfType<Token>().First();
            ServerPublicKey = responseObjects.OfType<ServerPublicKey>().First();
        }
    }
}
