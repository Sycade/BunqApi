using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class Alias : IBunqEntity
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        internal Alias() { }

        public Alias(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
