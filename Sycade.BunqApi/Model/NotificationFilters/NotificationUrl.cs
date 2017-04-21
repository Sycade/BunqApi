using Newtonsoft.Json;
using Sycade.BunqApi.Converters;

namespace Sycade.BunqApi.Model.NotificationFilters
{
    public class NotificationUrl<TObject> : BunqEntity
    {
        [JsonProperty("target_url")]
        public string TargetUrl { get; set; }
        [JsonProperty("category")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public NotificationFilterCategory Category { get; set; }
        [JsonProperty("event_type")]
        public string EventType { get; set; }

        [JsonProperty("object")]
        public TObject Object { get; set; }
    }
}
