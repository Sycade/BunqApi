using Sycade.BunqApi.Responses;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class SessionEndpoint : Endpoint
    {
        public SessionEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task DeleteAsync(Session session)
        {
            await ApiClient.DoSignedApiRequestAsync(HttpMethod.Delete, $"session/{session.Id.Value}", session.Token);
        }
    }
}
