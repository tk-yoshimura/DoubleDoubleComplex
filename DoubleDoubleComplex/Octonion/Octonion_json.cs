using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DoubleDoubleComplex {
    [JsonConverter(typeof(OctonionJsonConverter))]
    public partial class Octonion { }

    public class OctonionJsonConverter : JsonConverter<Octonion> {
        public override Octonion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            return Octonion.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, Octonion value, JsonSerializerOptions options) {
            writer.WriteStringValue(value.ToString());
        }
    }
}