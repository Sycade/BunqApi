using Sycade.BunqApi.Collections;
using Sycade.BunqApi.Model.Cards;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class CardEndpoint : Endpoint
    {
        public CardEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<CardDebit> GetByIdAsync(int cardId)
        {
            var session = ApiClient.Session;

            return await ApiClient.DoSignedApiRequestSingleAsync<CardDebit>(HttpMethod.Get, $"user/{session.User.Id}/card/{cardId}", session.Token);
        }

        public async Task<BunqCollection<CardDebit>> GetAllAsync()
        {
            var session = ApiClient.Session;

            return await ApiClient.DoSignedApiRequestAsync<CardDebit>(HttpMethod.Get, $"user/{session.User.Id}/card", session.Token);
        }
    }
}
