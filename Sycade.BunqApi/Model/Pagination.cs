using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class Pagination : BunqEntity
    {
        [JsonProperty("future_url")]
        public string FutureUrl { get; set; }
        [JsonProperty("newer_url")]
        public string NewerUrl { get; set; }
        [JsonProperty("older_url")]
        public string OlderUrl { get; set; }
    }
}
