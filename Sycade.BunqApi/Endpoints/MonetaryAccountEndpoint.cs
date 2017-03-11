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


        public async Task<MonetaryAccountBank> GetByIdAsync(User user, int monetaryAccountId, Token sessionToken)
        {
            var responseObjects = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{user.Id}/monetary-account-bank/{monetaryAccountId}", sessionToken);

            return responseObjects.Cast<MonetaryAccountBank>().First();
        }

        public async Task<MonetaryAccountBank[]> ListMonetaryAccountBanksAsync(User user, Token sessionToken)
        {
            var responseObjects = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{user.Id}/monetary-account-bank", sessionToken);

            return responseObjects.Cast<MonetaryAccountBank>().ToArray();
        }
    }
}
