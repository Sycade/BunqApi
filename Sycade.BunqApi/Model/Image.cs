using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Model
{
    public class Image : IBunqEntity
    {
        [JsonProperty("attachment_public_uuid")]
        public Guid AttachmentPublicUuid { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("content_type")]
        public string ContentType { get; set; }
    }
}
