using Sycade.BunqApi.Model;
using Sycade.BunqApi.Requests;
using Sycade.BunqApi.Responses;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class SessionServerEndpoint : Endpoint
    {
        public SessionServerEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<Session> CreateSessionAsync(string apiKey, Token installationToken)
        {
            var request = new CreateSessionServerRequest(apiKey);

            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Post, "session-server", installationToken, request);
            var response = new Session(entities);

            return response;
        }

        public async Task<Session> CreateSessionAsync(string apiKey, string installationToken)
        {
            return await CreateSessionAsync(apiKey, new Token(installationToken));
        }
    }
}
