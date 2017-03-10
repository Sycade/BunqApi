using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Model
{
    public class Avatar : IBunqEntity
    {
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
        [JsonProperty("image")]
        public Image[] Images { get; set; }
        [JsonProperty("anchor_uuid")]
        public Guid AnchorUuid { get; set; }
    }
}
