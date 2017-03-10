using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class MonetaryAccountBank : IBunqEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
