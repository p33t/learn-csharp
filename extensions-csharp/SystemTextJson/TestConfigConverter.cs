using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using extensions_csharp.SystemTextJson.Model;

namespace extensions_csharp.SystemTextJson
{
    public class TestConfigConverter : JsonConverter<TestConfig>
    {
        public override TestConfig Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerClone = reader;

            if (readerClone.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string? propertyName = readerClone.GetString();
            if (propertyName != "FormatVersion")
            {
                throw new JsonException();
            }

            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            var formatVersion = readerClone.GetInt32();

            return formatVersion switch
            {
                1 => JsonSerializer.Deserialize<TestConfigV1>(ref reader, options),
                _ => throw new Exception($"Unrecognised format version: {formatVersion}")
            };
        }

        public override void Write(Utf8JsonWriter writer, TestConfig value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}