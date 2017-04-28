using Newtonsoft.Json;
using Sycade.BunqApi.Model.Users;

namespace Sycade.BunqApi.Model.Invoices
{
    public class InvoiceAlias : BunqEntity
    {
        [JsonProperty("iban")]
        public string Iban { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }

        [JsonProperty("label_user")]
        public LabelUser LabelUser { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("bunq_me")]
        public Alias BunqMe { get; set; }
    }
}
