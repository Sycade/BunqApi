using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class Geolocation : BunqEntity
    {
        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }
        [JsonProperty("longitude")]
        public decimal Longitude { get; set; }
        [JsonProperty("altitude")]
        public decimal Altitude { get; set; }
        [JsonProperty("radius")]
        public decimal Radius { get; set; }

        internal Geolocation() { }
    }
}
