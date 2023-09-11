namespace Nucleus.Plugins.Enums {
    /// <summary>
    /// The type of Message
    /// </summary>
    public enum MessageKind {
        /// <summary>
        /// Message is a System message, not sent by a User
        ///   Could be response from an HTTP Call or a Notice of some kind
        /// </summary>
        SYSTEM,
        
        /// <summary>
        /// Message that is received from a User
        /// </summary>
        CHAT,
        
        /// <summary>
        /// An outgoing message from the UI input box
        /// </summary>
        SENT
    }
}
