using Newtonsoft.Json;
using Sycade.BunqApi.Converters;

namespace Sycade.BunqApi.Model
{
    public class Alias : BunqEntity
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public AliasType Type { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        internal Alias() { }

        public Alias(string ibanNumber)
        {
            Type = AliasType.IBAN;
            Value = ibanNumber;
        }

        public Alias(AliasType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
