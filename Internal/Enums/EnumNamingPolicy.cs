namespace Nucleus.Internal.Enums {
    internal enum EnumNamingPolicy {
        /// <summary>
        /// When printing the Enum, output it using the lowercase name
        /// </summary>
        LOWERCASE,
        
        /// <summary>
        /// When printing the Enum, output it using the uppercase name
        /// </summary>
        UPPERCASE,
        
        /// <summary>
        /// When printing the Enum, use the assigned unlying type (int32, int16, ...)
        /// </summary>
        UNDERLYING
    }
}
