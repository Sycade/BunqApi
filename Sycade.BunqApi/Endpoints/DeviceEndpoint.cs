using Sycade.BunqApi.Collections;
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

        public async Task<BunqCollection<Device>> GetAllAsync()
        {
            return await ApiClient.DoSignedApiRequestAsync<Device>(HttpMethod.Get, "device", ApiClient.Session.Token);
        }

        public async Task<Device> GetByIdAsync(int deviceId)
        {
            return await ApiClient.DoSignedApiRequestSingleAsync<Device>(HttpMethod.Get, $"device/{deviceId}", ApiClient.Session.Token);
        }
    }
}
