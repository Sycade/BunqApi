using Sycade.BunqApi.Model;
using Sycade.BunqApi.Requests;
using Sycade.BunqApi.Responses;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class PaymentEndpoint : Endpoint
    {
        public PaymentEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<Id> CreateAsync(int fromAccountId, Alias to, Amount amount, string description, Session session)
        {
            var request = new CreatePaymentRequest(amount, to, description);

            var responseObjects = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Post, $"user/{session.User.Id}/monetary-account/{fromAccountId}/payment", session.Token, request);

            return responseObjects.Cast<Id>().First();
        }
    }
}
