using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Model
{
    public class DirectorAlias : BunqEntity
    {
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }
        [JsonProperty("public_nick_name")]
        public string PublicNickName { get; set; }
    }
}
