﻿using Sycade.BunqApi.Collections;
using Sycade.BunqApi.Model;
using Sycade.BunqApi.Model.CustomerStatements;
using Sycade.BunqApi.Requests;
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


        public async Task<Id> CreateCsvAsync(long monetaryAccountId, DateTime startDate, DateTime endDate, CustomerStatementRegionalFormat regionalFormat)
        {
            var session = ApiClient.Session;

            var request = new CreateCustomerStatementRequest(startDate, endDate, CustomerStatementFormat.CSV, regionalFormat);

            return await ApiClient.DoSignedApiRequestSingleAsync<Id>(HttpMethod.Post, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement", session.Token, request);
        }

        public async Task<Id> CreateMt940Async(long monetaryAccountId, DateTime startDate, DateTime endDate)
        {
            var session = ApiClient.Session;

            var request = new CreateCustomerStatementRequest(startDate, endDate, CustomerStatementFormat.MT940);

            return await ApiClient.DoSignedApiRequestSingleAsync<Id>(HttpMethod.Post, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement", session.Token, request);
        }

        public async Task<Id> CreatePdfAsync(long monetaryAccountId, DateTime startDate, DateTime endDate)
        {
            var session = ApiClient.Session;

            var request = new CreateCustomerStatementRequest(startDate, endDate, CustomerStatementFormat.PDF);

            return await ApiClient.DoSignedApiRequestSingleAsync<Id>(HttpMethod.Post, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement", session.Token, request);
        }


        public async Task<BunqCollection<CustomerStatement>> GetAllAsync(long monetaryAccountId)
        {
            var session = ApiClient.Session;

            return await ApiClient.DoSignedApiRequestAsync<CustomerStatement>(HttpMethod.Get, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement", session.Token);
        }

        public async Task<CustomerStatement> GetByIdAsync(long monetaryAccountId, long customerStatementId)
        {
            var session = ApiClient.Session;

            return await ApiClient.DoSignedApiRequestSingleAsync<CustomerStatement>(HttpMethod.Get, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement/{customerStatementId}", session.Token);
        }

        public async Task<Stream> GetContentAsync(long monetaryAccountId, long customerStatementId)
        {
            var session = ApiClient.Session;

            return await ApiClient.DoRawApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/monetary-account/{monetaryAccountId}/customer-statement/{customerStatementId}/content", session.Token);
        }
    }
}
