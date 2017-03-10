using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Model
{
    public class UserCompany
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

        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("sub_status")]
        public string SubStatus { get; set; }

        [JsonProperty("public_uuid")]
        public Guid PublicUuid { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("public_nick_name")]
        public string PublicNickName { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("session_timeout")]
        public int SessionTimeout { get; set; }

        [JsonProperty("daily_limit_without_confirmation_login")]
        public Amount DailyLimitWithoutConfirmationLogin { get; set; }

        [JsonProperty("notification_filters")]
        public NotificationFilter[] NotificationFilters { get; set; }

        [JsonProperty("has_api_access")]
        public bool HasApiAccess { get; set; }

        [JsonProperty("address_main")]
        public Address AddressMain { get; set; }
        [JsonProperty("address_postal")]
        public Address AddressPostal { get; set; }

        [JsonProperty("counter_bank_iban")]
        public string CounterBankIban { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("chamber_of_commerce_number")]
        public string ChamberOfCommerceNumber { get; set; }

        [JsonProperty("director_alias")]
        public DirectorAlias DirectorAlias { get; set; }
        [JsonProperty("ubo")]
        public UltimateBeneficialOwner[] UltimateBeneficialOwners { get; set; }

        [JsonProperty("version_terms_of_service")]
        public string VersionTermsOfService { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
