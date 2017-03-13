using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Model
{
    public class UserCompany : User
    {
        [JsonProperty("counter_bank_iban")]
        public string CounterBankIban { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("chamber_of_commerce_number")]
        public string ChamberOfCommerceNumber { get; set; }

        [JsonProperty("director_alias")]
        public DirectorAlias DirectorAlias { get; set; }
        [JsonProperty("ubo")]
        public UltimateBeneficialOwner[] UltimateBeneficialOwners { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
