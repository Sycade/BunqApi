using Sycade.BunqApi.Model.MonetaryAccounts;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class MonetaryAccountEndpoint : Endpoint
    {
        public MonetaryAccountEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<MonetaryAccount> GetByIdAsync(int monetaryAccountId)
        {
            var session = ApiClient.Session;

            return await ApiClient.DoSignedApiRequestAsync<MonetaryAccount>(HttpMethod.Get, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}", session.Token);
        }

        public async Task<MonetaryAccount[]> GetAllAsync()
        {
            var session = ApiClient.Session;

            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/monetary-account", session.Token);

            return entities.Cast<MonetaryAccount>().ToArray();
        }
    }
}
