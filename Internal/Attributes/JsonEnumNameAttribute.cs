using Nucleus.Internal.Enums;

namespace Nucleus.Internal.Attributes {
    [AttributeUsage(AttributeTargets.Enum)]
    internal sealed class JsonEnumNameAttribute : Attribute {
        public EnumNamingPolicy Policy { get; init; }
    }
}
