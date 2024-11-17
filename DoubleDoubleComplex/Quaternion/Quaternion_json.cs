using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DoubleDoubleComplex {
    [JsonConverter(typeof(QuaternionJsonConverter))]
    public partial class Quaternion { }

    public class QuaternionJsonConverter : JsonConverter<Quaternion> {
        public override Quaternion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            return Quaternion.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, Quaternion value, JsonSerializerOptions options) {
            writer.WriteStringValue(value.ToString());
        }
    }
}