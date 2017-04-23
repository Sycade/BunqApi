using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Model
{
    public class Token : BunqEntity
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("token")]
        public string Value { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        internal Token() { }

        public Token(string value)
        {
            Value = value;
        }
    }
}
