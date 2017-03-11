using Sycade.BunqApi.Extensions;
using Sycade.BunqApi.Model;
using Sycade.BunqApi.Requests;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class InstallationEndpoint : Endpoint
    {
        public BunqApiClient ApiClient { get; }

        public InstallationEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<CreateInstallationResponse> CreateAsync()
        {
            var request = new CreateInstallationRequest(ApiClient.ClientCertificate.GetRSAPublicKey().AsPemString());

            var responseObjects = await ApiClient.DoApiRequestAsync(HttpMethod.Post, "installation", request);
            var response = new CreateInstallationResponse(responseObjects);

            return response;
        }
    }
}
