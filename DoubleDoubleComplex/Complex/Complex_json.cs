using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DoubleDoubleComplex {
    [JsonConverter(typeof(ComplexJsonConverter))]
    public partial class Complex { }

    public class ComplexJsonConverter : JsonConverter<Complex> {
        public override Complex Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            return Complex.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, Complex value, JsonSerializerOptions options) {
            writer.WriteStringValue(value.ToString());
        }
    }
}