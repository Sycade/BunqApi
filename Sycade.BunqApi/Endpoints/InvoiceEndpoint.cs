using Sycade.BunqApi.Model.Invoices;
using Sycade.BunqApi.Responses;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class InvoiceEndpoint : Endpoint
    {
        public InvoiceEndpoint(BunqApiClient apiClient)
            : base(apiClient)
        {
        }


        public async Task<Invoice[]> GetAllAsync(Session session)
        {
            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/invoice", session.Token);

            return entities.Cast<Invoice>().ToArray();
        }
    }
}
