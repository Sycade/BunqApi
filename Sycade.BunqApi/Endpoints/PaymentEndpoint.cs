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


        public async Task<Id> CreateAsync(User user, int fromAccountId, Alias toAccount, Amount amount, string description, Token sessionToken)
        {
            var request = new CreatePaymentRequest(amount, toAccount, description);

            var responseObjects = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Post, $"user/{user.Id}/monetary-account/{fromAccountId}/payment", sessionToken, request);

            return responseObjects.Cast<Id>().First();
        }
    }
}
