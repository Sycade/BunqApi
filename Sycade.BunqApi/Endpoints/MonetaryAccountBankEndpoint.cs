using Sycade.BunqApi.Collections;
using Sycade.BunqApi.Model.MonetaryAccounts;
using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class MonetaryAccountBankEndpoint : Endpoint
    {
        public MonetaryAccountBankEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<MonetaryAccountBank> GetByIdAsync(long monetaryAccountBankId)
        {
            var session = ApiClient.Session;

            return await ApiClient.DoSignedApiRequestSingleAsync<MonetaryAccountBank>(HttpMethod.Get, $"user/{session.User.Id}/monetary-account-bank/{monetaryAccountBankId}", session.Token);
        }

        public async Task<BunqCollection<MonetaryAccountBank>> GetAllAsync()
        {
            var session = ApiClient.Session;

            return await ApiClient.DoSignedApiRequestAsync<MonetaryAccountBank>(HttpMethod.Get, $"user/{session.User.Id}/monetary-account-bank", session.Token);
        }

        public async Task UpdatePropertyAsync<TProperty>(long monetaryAccountBankId, Expression<Func<MonetaryAccountBank, TProperty>> property, TProperty value)
        {
            var session = ApiClient.Session;

            await ApiClient.DoUpdatePropertyRequestAsync($"user/{session.User.Id}/monetary-account-bank/{monetaryAccountBankId}", property, value, session.Token);
        }
    }
}
