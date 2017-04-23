using Newtonsoft.Json;

namespace Sycade.BunqApi.Model.MonetaryAccounts
{
    public class Issuer
    {
        [JsonProperty("bic")]
        public string Bic { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
