namespace Nucleus.Plugins.Rendering {
    public struct Emote {
        /// <summary>
        /// The name/literal of the Emote
        /// </summary>
        public readonly string Name;
        
        /// <summary>
        /// The image source of the Emote
        /// </summary>
        public readonly Uri Uri;
        
        public Emote(string name, Uri uri) {
            if (uri is not {Scheme: "https"})
                throw new ArgumentException("Emote 'Uri' must be loaded over HTTPS", nameof(uri));
            this.Name = name ?? throw new ArgumentNullException(nameof(name), "Emote 'Name' is required");
            this.Uri = uri ?? throw new ArgumentNullException(nameof(uri), "Emote 'Uri' is required");
        }
    }
}
