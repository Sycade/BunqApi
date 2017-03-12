using Sycade.BunqApi.Model;
using Sycade.BunqApi.Responses;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class MonetaryAccountEndpoint : Endpoint
    {
        public MonetaryAccountEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<MonetaryAccount> GetByIdAsync(int monetaryAccountId, Session session)
        {
            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}", session.Token);

            return entities.Cast<MonetaryAccount>().First();
        }

        public async Task<MonetaryAccount[]> ListAsync(Session session)
        {
            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/monetary-account", session.Token);

            return entities.Cast<MonetaryAccount>().ToArray();
        }
    }
}
