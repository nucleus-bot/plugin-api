using System.Text.Json.Serialization;
using Nucleus.Internal.Json;

namespace Nucleus.Api {
    [Serializable]
    public sealed class PlatformDetails {
        /// <summary>
        /// The registered Platform-specific Id
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        
        /// <summary>
        /// The registered Platform-specific Name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        
        /// <summary>
        /// A Discriminator used to distinguish users on Platforms that allow multiple users to share the same username
        /// </summary>
        [JsonPropertyName("discriminator"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Discriminator { get; set; }
        
        /// <summary>
        /// The Platform
        /// </summary>
        [JsonPropertyName("site"), JsonConverter(typeof(JsonPlatformConverter))]
        public Payloads.Platform? Platform { get; set; }
    }
}
