using Newtonsoft.Json;

namespace Sycade.BunqApi.Model.MonetaryAccounts
{
    public class MonetaryAccountProfileFill
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("balance_preferred")]
        public Amount BalancePreferred { get; set; }
        [JsonProperty("balance_threshold_low")]
        public Amount BalanceThresholdLow { get; set; }
        [JsonProperty("method_fill")]
        public string MethodFill { get; set; }
        [JsonProperty("issuer")]
        public Issuer Issuer { get; set; }
    }
}
