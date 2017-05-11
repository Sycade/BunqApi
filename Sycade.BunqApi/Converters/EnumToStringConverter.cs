﻿using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Converters
{
    class EnumToStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType.IsEnum;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => Enum.Parse(objectType, (string)reader.Value);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
