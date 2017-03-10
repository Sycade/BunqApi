using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class Id
    {
        [JsonProperty("id")]
        public int Value { get; set; }
    }
}
