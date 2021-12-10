using System;
using System.IO;
using Newtonsoft.Json;

namespace BackupsExtra.Services
{
    public class BackupJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(FileInfo);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((FileInfo)value).FullName);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new FileInfo((string)reader.Value ?? string.Empty);
        }
    }
}