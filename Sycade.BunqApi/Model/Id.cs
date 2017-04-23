using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class Id : BunqEntity
    {
        [JsonProperty("id")]
        public long Value { get; set; }
    }
}
