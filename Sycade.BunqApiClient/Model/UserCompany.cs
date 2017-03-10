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
        [JsonProperty("public_uuid")]
        public Guid PublicUuid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("public_nick_name")]
        public string PublicDisplayName { get; set; }
        [JsonProperty("alias")]
        public Alias[] Aliases { get; set; }
    }
}
