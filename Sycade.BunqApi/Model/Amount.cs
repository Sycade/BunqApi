using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class Amount : IBunqEntity
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
