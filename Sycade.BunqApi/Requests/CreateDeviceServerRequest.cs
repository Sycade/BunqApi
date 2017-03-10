using Newtonsoft.Json;

namespace Sycade.BunqApi.Requests
{
    public class CreateDeviceServerRequest : IBunqApiRequest
    {
        [JsonProperty("description")]
        public string Description { get; }

        [JsonProperty("secret")]
        public string Secret { get; }

        public CreateDeviceServerRequest(string description, string secret)
        {
            Description = description;
            Secret = secret;
        }
    }
}
