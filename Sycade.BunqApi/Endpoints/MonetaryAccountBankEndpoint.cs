using Sycade.BunqApi.Model;
using Sycade.BunqApi.Responses;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class MonetaryAccountBankEndpoint : Endpoint
    {
        public MonetaryAccountBankEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<MonetaryAccountBank> GetAsync(int monetaryAccountBankId, Session session)
        {
            var responseObjects = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/monetary-account-bank/{monetaryAccountBankId}", session.Token);

            return responseObjects.Cast<MonetaryAccountBank>().First();
        }

        public async Task<MonetaryAccountBank[]> ListAsync(Session session)
        {
            var responseObjects = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/monetary-account-bank", session.Token);

            return responseObjects.Cast<MonetaryAccountBank>().ToArray();
        }
    }
}
