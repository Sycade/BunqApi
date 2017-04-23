using Newtonsoft.Json;

namespace Sycade.BunqApi.Requests
{
    class CreateSessionRequest : IBunqApiRequest
    {
        [JsonProperty("secret")]
        public string Secret { get; }

        public CreateSessionRequest(string apiKey)
        {
            Secret = apiKey;
        }
    }
}
