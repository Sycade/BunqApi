using Newtonsoft.Json;
using Sycade.BunqApi.Converters;
using System;

namespace Sycade.BunqApi.Model.Devices
{
    public class Device : BunqEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public DeviceStatus Status { get; set; }

        internal Device() { }
    }
}
