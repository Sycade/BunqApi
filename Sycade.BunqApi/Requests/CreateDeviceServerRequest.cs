using Newtonsoft.Json;
using System.Linq;
using System.Net;

namespace Sycade.BunqApi.Requests
{
    class CreateDeviceServerRequest : IBunqApiRequest
    {
        [JsonProperty("description")]
        public string Description { get; }
        [JsonProperty("secret")]
        public string Secret { get; }

        internal CreateDeviceServerRequest(string description, string secret)
        {
            Description = description;
            Secret = secret;
        }
    }
}
