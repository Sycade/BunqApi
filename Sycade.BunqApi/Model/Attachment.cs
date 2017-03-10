using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class Attachment : IBunqEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("content_type")]
        public string ContentType { get; set; }
    }
}
