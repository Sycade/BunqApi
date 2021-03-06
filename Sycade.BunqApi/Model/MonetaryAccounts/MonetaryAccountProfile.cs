﻿using Newtonsoft.Json;

namespace Sycade.BunqApi.Model.MonetaryAccounts
{
    public class MonetaryAccountProfile : BunqEntity
    {
        [JsonProperty("profile_fill")]
        public MonetaryAccountProfileFill ProfileFill { get; set; }
        [JsonProperty("profile_drain")]
        public string ProfileDrain { get; set; }
        [JsonProperty("profile_action_required")]
        public string ProfileActionRequired { get; set; }
        [JsonProperty("profile_amount_required")]
        public Amount ProfileAmountRequired { get; set; }
    }
}
