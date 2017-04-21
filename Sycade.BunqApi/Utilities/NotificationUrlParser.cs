using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sycade.BunqApi.Model.NotificationFilters;
using System;

namespace Sycade.BunqApi.Utilities
{
    public static class NotificationUrlParser
    {
        class ObjectConverter<TObject> : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(TObject);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var topObject = serializer.Deserialize<JObject>(reader);
                var actualObject = ((JProperty)topObject.First).Value as JObject;

                return actualObject.ToObject(objectType);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        public static NotificationUrl<TObject> Parse<TObject>(string content)
        {
            var topObject = JObject.Parse(content);
            var notificationUrlObject = ((JProperty)topObject.First).Value as JObject;

            var serializer = new JsonSerializer();
            serializer.Converters.Add(new ObjectConverter<TObject>());

            var notificationUrl = notificationUrlObject.ToObject<NotificationUrl<TObject>>(serializer);

            return notificationUrl;
        }
    }
}
