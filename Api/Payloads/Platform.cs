using System.Text.Json.Serialization;

namespace Nucleus.Api.Payloads {
    /// <summary>
    /// Represents a Platform/Website
    /// </summary>
    [Serializable]
    public class Platform {
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        
        public string Lower()
            => this.Name.ToLowerInvariant();
        
        public string Id()
            => this.Lower().Replace(' ', '_');
    }
}
