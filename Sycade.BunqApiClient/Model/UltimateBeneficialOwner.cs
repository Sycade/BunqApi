using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Model
{
    public class UltimateBeneficialOwner
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("date_of_birth")]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty("nationality")]
        public string Nationality { get; set; }
    }
}
