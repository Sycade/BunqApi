using Sycade.BunqApi.Model.Devices;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class DeviceEndpoint : Endpoint
    {
        public DeviceEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }

        public async Task<Device[]> GetAllAsync()
        {
            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, "device", ApiClient.Session.Token);

            return entities.Cast<Device>().ToArray();
        }

        public async Task<Device> GetByIdAsync(int deviceId)
        {
            return await ApiClient.DoSignedApiRequestAsync<Device>(HttpMethod.Get, $"device/{deviceId}", ApiClient.Session.Token);
        }
    }
}
