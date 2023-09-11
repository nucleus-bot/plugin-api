using System.Text.Json.Serialization;

namespace Nucleus.Api {
    public class PayloadPlatform {
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        
        public string Lower()
            => this.Name.ToLowerInvariant();
        
        public string Id()
            => this.Lower().Replace(' ', '_');
    }
}
