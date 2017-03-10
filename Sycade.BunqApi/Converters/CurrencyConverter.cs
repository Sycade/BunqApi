using Newtonsoft.Json;
using Sycade.BunqApi.Model;
using System;

namespace Sycade.BunqApi.Converters
{
    class CurrencyConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Currency);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return (Currency)Enum.Parse(typeof(Currency), (string)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
