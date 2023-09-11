namespace Nucleus.Plugins.Rendering {
    public interface IComponentsHelper {
        /// <summary>
        /// Creates a new <see cref="ITextContent"/>
        /// </summary>
        /// <returns></returns>
        ITextComponent Text(string content);
        
        /// <summary>
        /// Creates a new <see cref="IImageComponent"/>
        /// </summary>
        /// <returns></returns>
        IImageComponent Image(string literal, Uri image);
    }
}
