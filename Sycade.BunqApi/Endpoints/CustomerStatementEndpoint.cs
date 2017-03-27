using Sycade.BunqApi.Model;
using Sycade.BunqApi.Requests;
using Sycade.BunqApi.Responses;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class CustomerStatementEndpoint : Endpoint
    {
        public CustomerStatementEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task CreateCsvAsync(int monetaryAccountId, DateTime startDate, DateTime endDate, CustomerStatementRegionalFormat regionalFormat, Session session)
        {
            var request = new CreateCustomerStatementRequest(startDate, endDate, CustomerStatementFormat.CSV, regionalFormat);

            await ApiClient.DoSignedApiRequestAsync(HttpMethod.Post, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement", session.Token, request);
        }

        public async Task CreateMt940Async(int monetaryAccountId, DateTime startDate, DateTime endDate, Session session)
        {
            var request = new CreateCustomerStatementRequest(startDate, endDate, CustomerStatementFormat.MT940);

            await ApiClient.DoSignedApiRequestAsync(HttpMethod.Post, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement", session.Token, request);
        }

        public async Task CreatePdfAsync(int monetaryAccountId, DateTime startDate, DateTime endDate, Session session)
        {
            var request = new CreateCustomerStatementRequest(startDate, endDate, CustomerStatementFormat.PDF);

            await ApiClient.DoSignedApiRequestAsync(HttpMethod.Post, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement", session.Token, request);
        }


        public async Task<Stream> GetContentAsync(int monetaryAccountId, int customerStatementId, Session session)
        {
            return await ApiClient.DoRawApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement/{customerStatementId}/content", session.Token);
        }
    }
}
