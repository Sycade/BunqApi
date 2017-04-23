using Newtonsoft.Json;
using Sycade.BunqApi.Converters;
using Sycade.BunqApi.Model.Users;
using System;

namespace Sycade.BunqApi.Model.Cards
{
    public class LabelCard : BunqEntity
    {
        [JsonProperty("second_line")]
        public string SecondLine { get; set; }

        [JsonProperty("expiry_date")]
        public DateTime ExpiryDate { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public CardDebitStatus Status { get; set; }

        [JsonProperty("label_user")]
        public LabelUser LabelUser { get; set; }
    }
}
