using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class Setting : IBunqEntity
    {
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("default_avatar_status")]
        public string DefaultAvatarStatus { get; set; }
        [JsonProperty("restriction_chat")]
        public string RestrictionChat { get; set; }
    }
}
