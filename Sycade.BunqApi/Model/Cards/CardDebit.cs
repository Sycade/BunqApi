using Newtonsoft.Json;
using Sycade.BunqApi.Converters;
using Sycade.BunqApi.Model.MonetaryAccounts;
using System;

namespace Sycade.BunqApi.Model.Cards
{
    public class CardDebit : BunqEntity
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("second_line")]
        public string SecondLine { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public CardDebitStatus Status { get; set; }
        [JsonProperty("order_status")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public CardDebitOrderStatus OrderStatus { get; set; }

        [JsonProperty("expiry_date")]
        public DateTime ExpiryDate { get; set; }

        [JsonProperty("name_on_card")]
        public string NameOnCard { get; set; }

        [JsonProperty("limit")]
        public Limit[] Limits { get; set; }

        [JsonProperty("mag_stripe_permission")]
        public MagStripePermission MagStripePermission { get; set; }

        [JsonProperty("country_permission")]
        public CountryPermission[] CountryPermissions { get; set; }

        [JsonProperty("label_monetary_account_ordered")]
        public MonetaryAccountBank LabelMonetaryAccountOrdered { get; set; }
        [JsonProperty("label_monetary_account_current")]
        public MonetaryAccountBank LabelMonetaryAccountCurrent { get; set; }
    }
}
