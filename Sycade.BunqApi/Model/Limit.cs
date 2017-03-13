using Newtonsoft.Json;
using Sycade.BunqApi.Converters;

namespace Sycade.BunqApi.Model
{
    public class Limit : BunqEntity
    {
        [JsonProperty("daily_limit")]
        public int DailyLimit { get; set; }
        [JsonProperty("currency")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public Currency Currency { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
