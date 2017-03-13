using Sycade.BunqApi.Extensions;
using Sycade.BunqApi.Model;
using Sycade.BunqApi.Requests;
using Sycade.BunqApi.Responses;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class InstallationEndpoint : Endpoint
    {
        public InstallationEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<Installation> CreateAsync()
        {
            var request = new CreateInstallationRequest(ApiClient.ClientCertificate.GetRSAPublicKey().ToPemString());

            var entities = await ApiClient.DoApiRequestAsync(HttpMethod.Post, "installation", request);
            var response = new Installation(entities);

            return response;
        }
    }
}
