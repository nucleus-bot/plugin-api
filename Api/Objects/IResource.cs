using System.Text.Json.Serialization;

namespace Nucleus.Api.Objects {
    public interface IResource {
        [JsonIgnore]
        Guid? UUID { get; }
    }
}
