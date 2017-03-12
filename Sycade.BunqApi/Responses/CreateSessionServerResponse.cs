using Sycade.BunqApi.Endpoints;
using Sycade.BunqApi.Model;
using System.Linq;

namespace Sycade.BunqApi.Responses
{
    public class Session : IBunqInteractableEntity
    {
        private SessionEndpoint _sessionEndpoint;

        public Id Id { get; }
        public Token Token { get; }
        public User User { get; }

        internal Session(IBunqEntity[] entities)
        {
            Id = entities.OfType<Id>().First();
            Token = entities.OfType<Token>().First();
            User = entities.OfType<User>().First();
        }


        void IBunqInteractableEntity.Initialize(BunqHttpClient apiClient)
        {
            _sessionEndpoint = new SessionEndpoint(apiClient);
        }
    }
}
