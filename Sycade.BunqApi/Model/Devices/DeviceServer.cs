using Newtonsoft.Json;

namespace Sycade.BunqApi.Model.Devices
{
    public class DeviceServer : Device
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        internal DeviceServer() { }
    }
}
