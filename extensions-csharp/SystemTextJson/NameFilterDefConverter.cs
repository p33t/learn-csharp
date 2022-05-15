using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using extensions_csharp.SystemTextJson.Model;

namespace extensions_csharp.SystemTextJson
{
    public class NameFilterDefConverter : JsonConverter<NameFilterDef>
    {
        public override NameFilterDef Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            if (propertyName != nameof(NameFilterDef.Subtype))
            {
                throw new JsonException();
            }

            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            var subtype = readerClone.GetString();

            return subtype switch
            {
                nameof(EligibleList) => JsonSerializer.Deserialize<EligibleList>(ref reader, options),
                nameof(Prefixed) => JsonSerializer.Deserialize<Prefixed>(ref reader, options),
                _ => throw new Exception($"Unrecognised sub type: {subtype}")
            };
        }

        public override void Write(Utf8JsonWriter writer, NameFilterDef value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}