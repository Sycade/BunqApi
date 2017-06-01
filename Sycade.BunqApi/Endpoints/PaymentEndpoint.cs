using Sycade.BunqApi.Model;
using Sycade.BunqApi.Model.Payments;
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


        public async Task<Id> CreateAsync(long fromAccountId, Alias to, Amount amount, string description)
        {
            var session = ApiClient.Session;
            var request = new CreatePaymentRequest(amount, to, description);

            return await ApiClient.DoSignedApiRequestAsync<Id>(HttpMethod.Post, $"user/{session.User.Id}/monetary-account/{fromAccountId}/payment", session.Token, request);
        }

        public async Task<Payment[]> GetAllAsync(long monetaryAccountId)
        {
            var session = ApiClient.Session;

            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/payment", session.Token);

            return entities.Cast<Payment>().ToArray();
        }

        public async Task<Payment> GetByIdAsync(long paymentId, long monetaryAccountId)
        {
            var session = ApiClient.Session;

            return await ApiClient.DoSignedApiRequestAsync<Payment>(HttpMethod.Get, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/payment/{paymentId}", session.Token);
        }
    }
}
