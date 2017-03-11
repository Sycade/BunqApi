using Newtonsoft.Json;
using Sycade.BunqApi.Converters;
using System;

namespace Sycade.BunqApi.Model
{
    public abstract class MonetaryAccount
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("alias")]
        public Alias[] Aliases { get; set; }
        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }

        [JsonProperty("balance")]
        public Amount Balance { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("currency")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public Currency Currency { get; set; }

        [JsonProperty("daily_limit")]
        public Amount DailyLimit { get; set; }
        [JsonProperty("daily_spent")]
        public Amount DailySpent { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("public_uuid")]
        public Guid PublicUuid { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("sub_status")]
        public string SubStatus { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("monetary_account_profile")]
        public MonetaryAccountProfile MonetaryAccountProfile { get; set; }

        [JsonProperty("notification_filters")]
        public NotificationFilter[] NotificationFilters { get; set; }

        [JsonProperty("setting")]
        public Setting Setting { get; set; }

        [JsonProperty("overdraft_limit")]
        public Amount OverdraftLimit { get; set; }
    }
}
