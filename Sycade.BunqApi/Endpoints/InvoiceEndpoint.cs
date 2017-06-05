using Sycade.BunqApi.Collections;
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


        public async Task<BunqCollection<Invoice>> GetAllAsync()
        {
            var session = ApiClient.Session;

            return await ApiClient.DoSignedApiRequestAsync<Invoice>(HttpMethod.Get, $"user/{session.User.Id}/invoice", session.Token);
        }

        public async Task<Stream> GetPdfContentAsync(long invoiceId)
        {
            var session = ApiClient.Session;

            return await ApiClient.DoRawApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/invoice/{invoiceId}/pdf-content", session.Token);
        }
    }
}
