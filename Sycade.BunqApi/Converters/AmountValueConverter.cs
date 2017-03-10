using Newtonsoft.Json;
using Sycade.BunqApi.Model;
using System;
using System.Globalization;

namespace Sycade.BunqApi.Converters
{
    class AmountValueConverter : JsonConverter
    {
        private CultureInfo _usCultureInfo = new CultureInfo("en-US");

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Amount);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType != typeof(decimal))
                return reader.Value;

            return decimal.Parse((string)reader.Value, _usCultureInfo);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is decimal))
                writer.WriteValue(value);

            writer.WriteValue(((decimal)value).ToString(_usCultureInfo));
        }
    }
}
