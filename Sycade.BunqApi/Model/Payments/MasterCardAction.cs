using Newtonsoft.Json;
using Sycade.BunqApi.Model.Cards;
using System;

namespace Sycade.BunqApi.Model.Payments
{
    public class MasterCardAction : BunqEntity
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("monetary_account_id")]
        public long MonetaryAccountId { get; set; }
        [JsonProperty("card_id")]
        public long CardId { get; set; }

        [JsonProperty("amount_local")]
        public Amount AmountLocal { get; set; }
        [JsonProperty("amount_billing")]
        public Amount AmountBilling { get; set; }
        [JsonProperty("amount_original_local")]
        public Amount AmountOriginalLocal { get; set; }
        [JsonProperty("amount_original_billing")]
        public Amount AmountOriginalBilling { get; set; }
        [JsonProperty("amount_fee")]
        public Amount AmountFee { get; set; }

        [JsonProperty("decision")]
        public string Decision { get; set; }
        [JsonProperty("decision_description")]
        public string DecisionDescription { get; set; }
        [JsonProperty("decision_description_translated")]
        public string DecisionDescriptionTranslated { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("authorisation_status")]
        public string AuthorisationStatus { get; set; }
        [JsonProperty("authorisation_type")]
        public string AuthorisationType { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("alias")]
        public LabelMonetaryAccount Alias { get; set; }
        [JsonProperty("counterparty_alias")]
        public LabelMonetaryAccount CounterpartyAlias { get; set; }

        [JsonProperty("label_card")]
        public LabelCard LabelCard { get; set; }

        [JsonProperty("token_status")]
        public string TokenStatus { get; set; }

        [JsonProperty("reservation_expiry_time")]
        public string ReservationExpiryTime { get; set; }

        [JsonProperty("applied_limit")]
        public string AppliedLimit { get; set; }

        [JsonProperty("conversation")]
        public string Conversation { get; set; }

        [JsonProperty("allow_chat")]
        public bool AllowChat { get; set; }
    }
}
