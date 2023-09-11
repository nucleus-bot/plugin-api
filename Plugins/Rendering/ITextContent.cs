using System.Drawing;
using Nucleus.Plugins.Enums;

namespace Nucleus.Plugins.Rendering {
    public interface ITextContent : ITextComponent {
        /// <summary>
        /// Gets a named <see cref="IComponent"/> (using <see cref="StringComparer.OrdinalIgnoreCase"/>)
        /// <br />
        /// If <paramref name="name"/> is not defined, the value returned will be <see langword="null"/>
        /// </summary>
        /// <param name="name"></param>
        IComponent? this[string name] { get; }
        
        /// <summary>
        /// Get or Set components within the <see cref="ITextContent"/>
        /// <br />
        /// If <paramref name="componentIndex"/> is out of bounds, the value returned will be <see langword="null"/>
        /// </summary>
        /// <param name="componentIndex">An index</param>
        IComponent? this[int componentIndex] { get; set; }
        
        /// <summary>
        /// Get or Set components within the <see cref="ITextContent"/>
        /// </summary>
        /// <param name="range"></param>
        IComponent this[Range range] { get; set; }
        
        /// <summary>
        /// Get or Set <see cref="Color"/>s of the Background. Applying a <see cref="ITextComponent.BackgroundColor"/> will reset the gradient
        /// The <see cref="double"/> value should be between 0.0d (0%) and 1.0d (100%)
        /// </summary>
        IDictionary<double, Color> BackgroundColors { get; }
        
        /// <summary>
        /// Returns the number of <see cref="IComponent"/>s
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Replace the nth <see cref="IComponent"/> with an <see cref="IImageComponent"/>
        /// </summary>
        /// <param name="componentIndex"></param>
        /// <param name="imageSource"></param>
        /// <param name="name"><see cref="StringComparer.OrdinalIgnoreCase"/> name for the <see cref="IImageComponent"/> for easy fetching (Default names present in <see cref="MessageProperty"/>)</param>
        void ReplaceImage(int componentIndex, Uri imageSource, string? name = null);
        
        /// <summary>
        /// Replace part of the <see cref="ITextContent"/> with an <see cref="IComponent"/>
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="component"></param>
        /// <param name="name"><see cref="StringComparer.OrdinalIgnoreCase"/> name for the <paramref name="component"/> for easy fetching (Default names present in <see cref="MessageProperty"/>)</param>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="startIndex"/> is out of bounds</exception>
        void Replace(int startIndex, int length, IComponent component, string? name = null);
        
        /// <summary>
        /// Replace part of the <see cref="ITextContent"/> with an <see cref="IImageComponent"/>
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="imageSource"></param>
        /// <param name="name"><see cref="StringComparer.OrdinalIgnoreCase"/> name for the <see cref="IImageComponent"/> for easy fetching (Default names present in <see cref="MessageProperty"/>)</param>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="startIndex"/> is out of bounds</exception>
        void ReplaceImage(int startIndex, int length, Uri imageSource, string? name = null);
        
        /// <summary>
        /// Replace part of the <see cref="ITextContent"/> with an <see cref="IImageComponent"/>
        /// </summary>
        /// <param name="range"></param>
        /// <param name="imageSource"></param>
        /// <param name="name"><see cref="StringComparer.OrdinalIgnoreCase"/> name for the <see cref="IImageComponent"/> for easy fetching (Default names present in <see cref="MessageProperty"/>)</param>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="range"/> is out of bounds</exception>
        void ReplaceImage(Range range, Uri imageSource, string? name = null);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="component"></param>
        /// <param name="name"><see cref="StringComparer.OrdinalIgnoreCase"/> name for the <paramref name="component"/> for easy fetching (Default names present in <see cref="MessageProperty"/>)</param>
        void Insert(int index, IComponent component, string? name = null);
        
        /// <summary>
        /// Append an <see cref="IComponent"/> to the end of the <see cref="ITextContent"/>
        /// </summary>
        /// <param name="component"></param>
        /// <param name="name"><see cref="StringComparer.OrdinalIgnoreCase"/> name for the <paramref name="component"/> for easy fetching (Default names present in <see cref="MessageProperty"/>)</param>
        void Append(IComponent component, string? name = null);
    }
}
