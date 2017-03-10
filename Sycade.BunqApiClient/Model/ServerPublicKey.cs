using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class ServerPublicKey
    {
        [JsonProperty("server_public_key")]
        public string Value { get; set; }
    }
}
