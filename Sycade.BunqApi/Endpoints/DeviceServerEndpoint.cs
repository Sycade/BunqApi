using Sycade.BunqApi.Model;
using Sycade.BunqApi.Requests;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class DeviceServerEndpoint : Endpoint
    {
        public DeviceServerEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }


        public async Task<Id> CreateAsync(string description, Token installationToken, params string[] permittedIps)
        {
            var request = new CreateDeviceServerRequest(description, ApiClient.ApiKey, permittedIps);

            return await ApiClient.DoSignedApiRequestAsync<Id>(HttpMethod.Post, "device-server", installationToken, request);
        }
    }
}
