using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nucleus.Internal.Json {
    internal sealed class JsonPlatformConverter : JsonConverter<Api.Payloads.Platform> {
        /// <inheritdoc />
        public override Api.Payloads.Platform? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => throw new NotImplementedException();
        
        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, Api.Payloads.Platform value, JsonSerializerOptions options) {
            throw new NotImplementedException();
        }
    }
}
