using Sycade.BunqApi.Model.Invoices;
using System.IO;
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


        public async Task<Invoice[]> GetAllAsync()
        {
            var session = ApiClient.Session;

            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/invoice", session.Token);

            return entities.Cast<Invoice>().ToArray();
        }

        public async Task<Stream> GetPdfContentAsync(long invoiceId)
        {
            var session = ApiClient.Session;

            return await ApiClient.DoRawApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/invoice/{invoiceId}/pdf-content", session.Token);
        }
    }
}
