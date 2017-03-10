using Newtonsoft.Json;

namespace Sycade.BunqApi.Requests
{
    public class CreateInstallationRequest : IBunqApiRequest
    {
        [JsonProperty("client_public_key")]
        public string ClientPublicKey { get; }

        public CreateInstallationRequest(string clientPublicKey)
        {
            ClientPublicKey = clientPublicKey;
        }
    }
}
