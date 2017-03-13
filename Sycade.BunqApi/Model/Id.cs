using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class Id : BunqEntity
    {
        [JsonProperty("id")]
        public int Value { get; set; }
    }
}
