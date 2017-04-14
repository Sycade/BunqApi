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
        [JsonProperty("permitted_ips")]
        public string[] PermittedIps { get; set; }

        internal CreateDeviceServerRequest(string description, string secret, string[] permittedIps)
        {
            Description = description;
            Secret = secret;
        }
    }
}
