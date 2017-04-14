using Newtonsoft.Json;
using Sycade.BunqApi.Converters;

namespace Sycade.BunqApi.Model.NotificationFilters
{
    public class NotificationFilter : BunqEntity
    {
        [JsonProperty("notification_delivery_method")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public NotificationFilterDeliveryMethod DeliveryMethod { get; set; }

        [JsonProperty("notification_target")]
        public string Target { get; set; }

        [JsonProperty("category")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public NotificationFilterCategory Category { get; set; }
    }
}
