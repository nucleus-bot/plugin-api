namespace Nucleus.Plugins.Rendering {
    public interface IImageComponent : IComponent {
        /// <summary>
        /// The width of the image. If <see langword="null"/> will copy the <see cref="Height"/>
        /// </summary>
        double? Width { get; set; }
        
        /// <summary>
        /// The height of the image. If <see langword="null"/> will copy the font size
        /// </summary>
        double? Height { get; set; }
        
        /// <summary>
        /// The Image Uri
        /// </summary>
        Uri Uri { get; }
        
        /// <summary>
        /// The Host of the Image URL
        /// </summary>
        string Host { get; }
        
        /// <summary>
        /// The Path of the Image on the Host
        /// </summary>
        string Path { get; }
    }
}
