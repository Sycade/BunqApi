using Newtonsoft.Json;
using Sycade.BunqApi.Model;

namespace Sycade.BunqApi.Requests
{
    class CreatePaymentRequest : IBunqApiRequest
    {
        [JsonProperty("amount")]
        public Amount Amount { get; set; }
        [JsonProperty("counterparty_alias")]
        public Alias CounterpartyAlias { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        public CreatePaymentRequest(Amount amount, Alias counterpartyAlias, string description)
        {
            Amount = amount;
            CounterpartyAlias = counterpartyAlias;
            Description = description;
        }
    }
}
