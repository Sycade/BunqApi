using Sycade.BunqApi.Model.Devices;
using Sycade.BunqApi.Responses;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Endpoints
{
    public class DeviceEndpoint : Endpoint
    {
        public DeviceEndpoint(BunqApiClient apiClient)
            : base(apiClient) { }

        public async Task<Device[]> GetAllAsync(Session session)
        {
            var entities = await ApiClient.DoSignedApiRequestAsync(HttpMethod.Get, "device", session.Token);

            return entities.Cast<Device>().ToArray();
        }

        public async Task<Device> GetByIdAsync(int deviceId, Session session)
        {
            return await ApiClient.DoSignedApiRequestAsync<Device>(HttpMethod.Get, $"device/{deviceId}", session.Token);
        }
    }
}
