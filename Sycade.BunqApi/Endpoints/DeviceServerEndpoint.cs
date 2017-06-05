using Sycade.BunqApi.Model;
using Sycade.BunqApi.Requests;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class DeviceServerEndpoint : Endpoint
    {
        public DeviceServerEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<Id> CreateAsync(string description, string apiKey, Token installationToken, params string[] permittedIps)
        {
            var request = new CreateDeviceServerRequest(description, apiKey, permittedIps);

            return await ApiClient.DoSignedApiRequestSingleAsync<Id>(HttpMethod.Post, "device-server", installationToken, request);
        }
    }
}
