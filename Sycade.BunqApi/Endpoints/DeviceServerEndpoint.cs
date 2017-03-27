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


        public async Task<Id> CreateAsync(string description, Token installationToken)
        {
            var request = new CreateDeviceServerRequest(description, ApiClient.ApiKey);

            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Post, "device-server", installationToken, request);

            return entities.Cast<Id>().First();
        }
    }
}
