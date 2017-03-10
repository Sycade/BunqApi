using Newtonsoft.Json;

namespace Sycade.BunqApi.Model
{
    public class Error
    {
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
        [JsonProperty("error_description_translated")]
        public string ErrorDescriptionTranslated { get; set; }
    }
}
