using System.Text.Json;

namespace education_infrastructure.Extensions
{
    public static class JsonExtension
    {
        public static string ToJSON(this object serializedObject)
        {
            JsonSerializerOptions serializerSettings = new JsonSerializerOptions();
            serializerSettings.Converters.Add(new StringConverter());
            return JsonSerializer.Serialize(serializedObject, serializerSettings);
        }

        public static string ToJSON(this object serializedObject, JsonSerializerOptions serializerSettings)
        {
            return JsonSerializer.Serialize(serializedObject, serializerSettings);
        }

        public static T DeserializeJSON<T>(this string json, JsonSerializerOptions jsonSerializerOptions = null)
        {
            return JsonSerializer.Deserialize<T>(json, jsonSerializerOptions);
        }

        public static object DeserializeJSON(this string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type);
        }

    }
}
