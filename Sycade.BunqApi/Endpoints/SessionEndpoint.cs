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


        public async Task StartAsync(string apiKey, Token installationToken)
        {
            var request = new CreateSessionRequest(apiKey);

            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Post, "session-server", installationToken, request);

            ApiClient.Session = new Session(entities);
        }

        public async Task StartAsync(string apiKey, string installationToken)
        {
            await StartAsync(apiKey, new Token(installationToken));
        }

        public async Task StopAsync()
        {
            var session = ApiClient.Session;

            await ApiClient.DoSignedApiRequestAsync(HttpMethod.Delete, $"session/{session.Id.Value}", session.Token);
        }
    }
}
