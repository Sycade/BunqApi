using Sycade.BunqApi.Model;
using System.Linq;

namespace Sycade.BunqApi.Responses
{
    public class CreateSessionServerResponse
    {
        public Id Id { get; }
        public Token Token { get; }
        public User User { get; }

        public CreateSessionServerResponse(IBunqEntity[] responseObjects)
        {
            Id = responseObjects.OfType<Id>().First();
            Token = responseObjects.OfType<Token>().First();
            User = responseObjects.OfType<User>().First();
        }
    }
}
