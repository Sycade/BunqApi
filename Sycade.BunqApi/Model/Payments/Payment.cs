using Newtonsoft.Json;
using Sycade.BunqApi.Converters;
using Sycade.BunqApi.Model.MonetaryAccounts;
using System;

namespace Sycade.BunqApi.Model.Payments
{
    public class Payment : BunqEntity
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("monetary_account_id")]
        public long MonetaryAccountId { get; set; }
        [JsonProperty("amount")]
        public Amount Amount { get; set; }

        [JsonProperty("alias")]
        public LabelMonetaryAccount Alias { get; set; }
        [JsonProperty("counterparty_alias")]
        public LabelMonetaryAccount CounterpartyAlias { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public PaymentType Type { get; set; }
        [JsonProperty("sub_type")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public PaymentSubType? SubType { get; set; }

        [JsonProperty("bunqto_status")]
        public string BunqToStatus { get; set; }
        [JsonProperty("bunqto_sub_status")]
        public string BunqToSubStatus { get; set; }
        [JsonProperty("bunqto_share_url")]
        public string BunqToShareUrl { get; set; }
        [JsonProperty("bunqto_expiry")]
        public DateTime BunqToExpiry { get; set; }
        [JsonProperty("bunqto_time_responded")]
        public DateTime BunqToTimeResponded { get; set; }

        [JsonProperty("attachment")]
        public Attachment[] Attachments { get; set; }

        [JsonProperty("merchant_reference")]
        public string MerchantReference { get; set; }

        [JsonProperty("batch_id")]
        public int? BatchId { get; set; }
        [JsonProperty("scheduled_id")]
        public int? ScheduledId { get; set; }

        [JsonProperty("address_shipping")]
        public Address AddressShipping { get; set; }
        [JsonProperty("address_billing")]
        public Address AddressBilling { get; set; }

        [JsonProperty("geolocation")]
        public Geolocation Geolocation { get; set; }

        [JsonProperty("allow_chat")]
        public bool AllowChat { get; set; }

        internal Payment() { }
    }
}
