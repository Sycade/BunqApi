using Sycade.BunqApi.Model;
using Sycade.BunqApi.Model.CustomerStatements;
using Sycade.BunqApi.Requests;
using Sycade.BunqApi.Responses;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class CustomerStatementEndpoint : Endpoint
    {
        public CustomerStatementEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<Id> CreateCsvAsync(long monetaryAccountId, DateTime startDate, DateTime endDate, CustomerStatementRegionalFormat regionalFormat, Session session)
        {
            var request = new CreateCustomerStatementRequest(startDate, endDate, CustomerStatementFormat.CSV, regionalFormat);

            return await ApiClient.DoSignedApiRequestAsync<Id>(HttpMethod.Post, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement", session.Token, request);
        }

        public async Task<Id> CreateMt940Async(long monetaryAccountId, DateTime startDate, DateTime endDate, Session session)
        {
            var request = new CreateCustomerStatementRequest(startDate, endDate, CustomerStatementFormat.MT940);

            return await ApiClient.DoSignedApiRequestAsync<Id>(HttpMethod.Post, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement", session.Token, request);
        }

        public async Task<Id> CreatePdfAsync(long monetaryAccountId, DateTime startDate, DateTime endDate, Session session)
        {
            var request = new CreateCustomerStatementRequest(startDate, endDate, CustomerStatementFormat.PDF);

            return await ApiClient.DoSignedApiRequestAsync<Id>(HttpMethod.Post, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement", session.Token, request);
        }


        public async Task<CustomerStatement[]> GetAllAsync(long monetaryAccountId, Session session)
        {
            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement", session.Token);

            return entities.Cast<CustomerStatement>().ToArray();
        }

        public async Task<CustomerStatement> GetByIdAsync(long monetaryAccountId, long customerStatementId, Session session)
        {
            return await ApiClient.DoSignedApiRequestAsync<CustomerStatement>(HttpMethod.Get, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement/{customerStatementId}", session.Token);
        }

        public async Task<Stream> GetContentAsync(long monetaryAccountId, long customerStatementId, Session session)
        {
            return await ApiClient.DoRawApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement/{customerStatementId}/content", session.Token);
        }
    }
}
