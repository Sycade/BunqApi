using Sycade.BunqApi.Model;
using Sycade.BunqApi.Requests;
using Sycade.BunqApi.Responses;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class SessionEndpoint : Endpoint
    {
        public SessionEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<Session> CreateAsync(string apiKey, Token installationToken)
        {
            var request = new CreateSessionRequest(apiKey);

            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Post, "session-server", installationToken, request);
            var response = new Session(entities);

            return response;
        }

        public async Task<Session> CreateAsync(string apiKey, string installationToken)
        {
            return await CreateAsync(apiKey, new Token(installationToken));
        }

        public async Task DeleteAsync(Session session)
        {
            await ApiClient.DoSignedApiRequestAsync(HttpMethod.Delete, $"session/{session.Id.Value}", session.Token);
        }
    }
}
