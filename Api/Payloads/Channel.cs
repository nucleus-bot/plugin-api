using System.Text.Json.Serialization;

namespace Nucleus.Api.Payloads {
    /// <summary>
    /// Represents a Channel on a <see cref="Platform"/>
    /// </summary>
    [Serializable]
    public class Channel : User {
        /// <summary>
        /// The Channels currently Stream
        /// </summary>
        [JsonPropertyName("stream"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public LiveStream? Stream { get; set; } = null;
    }
}
