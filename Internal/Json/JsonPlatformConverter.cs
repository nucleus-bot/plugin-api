using System.Text.Json;
using System.Text.Json.Serialization;
using Nucleus.Api;

namespace Nucleus.Internal.Json {
    internal sealed class JsonPlatformConverter : JsonConverter<PayloadPlatform> {
        /// <inheritdoc />
        public override PayloadPlatform? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => throw new NotImplementedException();
        
        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, PayloadPlatform value, JsonSerializerOptions options) {
            throw new NotImplementedException();
        }
    }
}
