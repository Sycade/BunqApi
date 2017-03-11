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


        public async Task<CreateSessionServerResponse> CreateAsync(Token installationToken)
        {
            var request = new CreateSessionServerRequest(ApiClient.ApiKey);

            var responseObjects = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Post, "session-server", installationToken, request);
            var response = new CreateSessionServerResponse(responseObjects);

            return response;
        }
    }
}
