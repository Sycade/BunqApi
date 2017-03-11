using Sycade.BunqApi.Model;
using Sycade.BunqApi.Requests;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class PaymentEndpoint : Endpoint
    {
        public PaymentEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<Id> CreateAsync(int fromAccountId, Alias to, Amount amount, string description, User user, Token sessionToken)
        {
            var request = new CreatePaymentRequest(amount, to, description);

            var responseObjects = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Post, $"user/{user.Id}/monetary-account/{fromAccountId}/payment", sessionToken, request);

            return responseObjects.Cast<Id>().First();
        }
    }
}
