using Sycade.BunqApi.Model;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class MonetaryAccountEndpoint : Endpoint
    {
        public MonetaryAccountEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<MonetaryAccount> GetByIdAsync(User user, int monetaryAccountId, Token sessionToken)
        {
            var responseObjects = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{user.Id}/monetary-account/{monetaryAccountId}", sessionToken);

            return responseObjects.Cast<MonetaryAccount>().First();
        }

        public async Task<MonetaryAccount[]> ListAsync(User user, Token sessionToken)
        {
            var responseObjects = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{user.Id}/monetary-account", sessionToken);

            return responseObjects.Cast<MonetaryAccount>().ToArray();
        }
    }
}
