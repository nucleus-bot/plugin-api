using System.Security.Cryptography;
using Nucleus.Api.Enums;

namespace Nucleus.Helpers {
    public static class Validation {
        public static HashAlgorithm GetAlgorithm(this HashType type) => type switch {
            HashType.SHA1 => SHA1.Create(),
            HashType.SHA256 => SHA256.Create(),
            HashType.SHA384 => SHA384.Create(),
            HashType.SHA512 => SHA512.Create(),
            HashType.MD5 => MD5.Create(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
