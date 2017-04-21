using Sycade.BunqApi.Model.MonetaryAccounts;
using Sycade.BunqApi.Responses;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class MonetaryAccountBankEndpoint : Endpoint
    {
        public MonetaryAccountBankEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<MonetaryAccountBank> GetByIdAsync(long monetaryAccountBankId, Session session)
        {
            return await ApiClient.DoSignedApiRequestAsync<MonetaryAccountBank>(HttpMethod.Get, $"user/{session.User.Id}/monetary-account-bank/{monetaryAccountBankId}", session.Token);
        }

        public async Task<MonetaryAccountBank[]> GetAllAsync(Session session)
        {
            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, $"user/{session.User.Id}/monetary-account-bank", session.Token);

            return entities.Cast<MonetaryAccountBank>().ToArray();
        }

        public async Task UpdatePropertyAsync<TProperty>(long monetaryAccountBankId, Expression<Func<MonetaryAccountBank, TProperty>> property, object value, Session session)
        {
            await ApiClient.DoUpdatePropertyRequestAsync($"user/{session.User.Id}/monetary-account-bank/{monetaryAccountBankId}", property, value, session.Token);
        }
    }
}
