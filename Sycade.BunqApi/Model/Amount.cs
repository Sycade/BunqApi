using Newtonsoft.Json;
using Sycade.BunqApi.Converters;

namespace Sycade.BunqApi.Model
{
    public class Amount : IBunqEntity
    {
        [JsonProperty("currency")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public Currency Currency { get; set; }

        [JsonProperty("value")]
        [JsonConverter(typeof(DecimalToStringConverter))]
        public decimal Value { get; set; }

        internal Amount() { }

        public Amount(Currency currency, decimal value)
        {
            Currency = currency;
            Value = value;
        }
    }
}
