using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class NotificationFilter : BunqEntity
    {
        [JsonProperty("notification_delivery_method")]
        public string NotificationDeliveryMethod { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
    }
}
