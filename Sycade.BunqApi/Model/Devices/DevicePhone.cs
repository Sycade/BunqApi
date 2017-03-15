using Newtonsoft.Json;

namespace Sycade.BunqApi.Model.Devices
{
    public class DevicePhone : Device
    {
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("os")]
        public string Os { get; set; }

        internal DevicePhone() { }
    }
}
