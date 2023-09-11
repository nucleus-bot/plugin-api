using System.Drawing;

namespace Nucleus.Plugins.Rendering {
    /// <summary>
    /// Individual text component, can write other text components or images (Emotes/etc)
    /// 
    /// Each individual text component can have a font, color, etc
    /// </summary>
    public interface ITextComponent : IComponent {
        /// <summary>
        /// Set a Color for the Text
        /// </summary>
        Color? TextColor { get; set; }
        
        /*/// <summary>
        /// Set a Color for the Shadow
        /// </summary>
        Color? ShadowColor { get; set; }*/
        
        /// <summary>
        /// Set a Color for the Background
        /// </summary>
        Color? BackgroundColor { get; set; }
        
        /// <summary>
        /// The Text component is considered decoration and can ignore parsing
        /// <br />
        /// Mainly useful for Username-Message-Separator (": "), TimeStamp, etc
        /// </summary>
        bool IsDecoration { get; set; }
        
        /// <summary>
        /// Controls the font size of the content
        /// </summary>
        double FontSize { get; set; }
        
        /// <summary>
        /// The <see cref="IComponent.Literal"/> FontWeight, defined in multiples of 100 (100-900), or (1-9)
        /// </summary>
        int FontWeight { get; set; }
        
        /// <summary>
        /// Draws a line under the <see cref="IComponent.Literal"/> content with a small padding
        /// </summary>
        bool Underline { get; set; }
        
        /// <summary>
        /// Draws a line under the <see cref="IComponent.Literal"/> content
        /// </summary>
        bool Baseline { get; set; }
        
        /// <summary>
        /// Draws a line over the <see cref="IComponent.Literal"/> content
        /// </summary>
        bool Overline { get; set; }
        
        /// <summary>
        /// Draws a line through the <see cref="IComponent.Literal"/> content
        /// </summary>
        bool Strikethrough { get; set; }
        
        /// <summary>
        /// Italicizes the <see cref="IComponent.Literal"/> content
        /// </summary>
        bool Italic { get; set; }
    }
}
