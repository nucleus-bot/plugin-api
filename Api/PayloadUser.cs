using System.Text.Json.Serialization;
using Nucleus.Api.Enums;
using Nucleus.Api.Objects;

namespace Nucleus.Api {
    [Serializable]
    public class PayloadUser : IResource {
        /// <summary>The Nucleus UUID of this Resource</summary>
        [JsonPropertyName("id")]
        public Guid? UUID { get; init; }
        
        /// <summary>The Nucleus UUID that owns this Resource</summary>
        [JsonPropertyName("owner")]
        public Guid? Owner { get; init; }
        
        /// <summary>The Identifier for the Resource on the *Platform*</summary>
        [JsonIgnore]
        public string? Id {
            set => this.PlatformDetails.Id = value;
            get => this.PlatformDetails.Id;
        }
        
        /// <summary>The Name for the Resource on the *Platform*</summary>
        [JsonIgnore]
        public string? Name {
            set => this.PlatformDetails.Name = value;
            get => this.PlatformDetails.Name;
        }
        public string? Lower() => this.Name?.ToLower();
        
        /// <summary>The Platform that this Resource belongs to</summary>
        [JsonIgnore]
        public PayloadPlatform? Platform {
            set => this.PlatformDetails.Platform = value;
            get => this.PlatformDetails.Platform;
        }
        
        /// <summary>The Platform details of this Resource</summary>
        [JsonPropertyName("platform")]
        public required PlatformDetails PlatformDetails { get; init; }
        
        /*/// <summary>Settings related to the User</summary>
        [JsonPropertyName("settings"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual IUserProperties Properties { get; set; }*/
        
        /// <summary>Roles that the User has (If viewed in the context of a channel)</summary>
        [JsonPropertyName("roles"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual UserRoles[]? Roles { get; set; }
    }
}
