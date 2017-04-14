using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Model.Cards
{
    public class MagStripePermission : BunqEntity
    {
        [JsonProperty("expiry_time")]
        public DateTime ExpiryTime { get; set; }
    }
}
