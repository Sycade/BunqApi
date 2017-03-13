using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Model
{
    public class CountryPermission : BunqEntity
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("expiry_time")]
        public DateTime ExpiryTime { get; set; }
    }
}
