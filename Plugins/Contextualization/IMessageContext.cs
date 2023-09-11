using Nucleus.Plugins.Enums;
using Nucleus.Plugins.Rendering;

namespace Nucleus.Plugins.Contextualization {
    /// <summary>
    /// The context of a Message when populating 
    /// </summary>
    public interface IMessageContext {
        /// <summary>
        /// The Message type
        /// </summary>
        MessageKind Kind { get; }
        
        /// <summary>
        /// The time that the message was sent in UTC (Coordinated Universal Time) / GMT (Greenwich Mean Time)
        /// </summary>
        DateTimeOffset UtcTime { get; }
        
        /// <summary>
        /// The time that the message was send in the Local System Time
        /// </summary>
        DateTimeOffset LocalTime { get; }
        
        #region Contexts
        
        IChannelContext? Channel { get; }
        
        IUserContext? Sender { get; }
        
        #endregion
        #region UI Components
        
        /// <summary>
        /// Reads the <see cref="MessageProperty.USERNAME"/> property from the <see cref="Contents"/>
        /// </summary>
        ITextComponent? Username { get; }
        
        /// <summary>
        /// <para>The content of the message being sent to the chat window</para>
        /// <para>If the <see cref="Kind"/> is <see cref="MessageKind.CHAT"/> then the first element will be the <see cref="Username"/>. Styling only the Username can be done by accessing the <see cref="Username"/> property, the remaining components can be iterated</para>
        /// <para><see cref="ITextContent"/> is composed of <see cref="IComponent"/> types (eg; <see cref="ITextComponent"/>, <see cref="IImageComponent"/>)</para>
        /// </summary>
        ITextContent? Contents { get; }
        
        #endregion
        #region Rendering
        
        /// <summary>
        /// The Text alignment
        /// 
        /// </summary>
        Alignment HorizontalAlignment { get; set; }
        
        #endregion
        #region Helpers
        
        /// <summary>
        /// Get contents of the message that the user sent (Present in <see cref="Contents"/> after the <see cref="Username"/>)
        /// </summary>
        /// <returns></returns>
        static IEnumerable<IComponent> GetMessageComponents(IMessageContext context) {
            ITextComponent? after = context.Username;
            bool passed = after is null;
            
            if (context.Contents is null)
                yield break;
            
            int length = context.Contents.Count;
            for (int i = 0; i < length; i++) {
                if (context.Contents[i] is not IComponent component)
                    yield break;
                
                if (!passed) {
                    if (object.ReferenceEquals(after, component))
                        passed = true;
                    continue;
                }
                
                yield return component;
            }
        }
        
        static IEnumerable<(Emote Emote, int Count)> GetEmotes(IMessageContext context) {
            if (context.Contents is null)
                yield break;
            
            IDictionary<IImageComponent, int> images = new Dictionary<IImageComponent, int>();
            
            int length = context.Contents.Count;
            for (int i = 0; i < length; i++) {
                if (context.Contents[i] is not IImageComponent image)
                    continue;
                
                // Get the current count of the emotes
                if (!images.TryGetValue(image, out int count))
                    count = 0;
                
                images[image] = count + 1;
            }
            
            // Yield each emote
            foreach ((IImageComponent image, int count) in images)
                yield return (new Emote(image.Literal, image.Uri), count);
        }
        
        /// <summary>
        /// Checks if the <see cref="Contents"/> (Excluding Username) are made entirely of Images (or Emotes)
        /// </summary>
        /// <returns>If the message only consists of Images</returns>
        static bool IsImages(IMessageContext context) {
            bool test = false;
            
            foreach (IComponent component in IMessageContext.GetMessageComponents(context)) {
                if (component is IImageComponent) {
                    test = true;
                } else if (component is ITextComponent text && (text.IsDecoration || string.IsNullOrWhiteSpace(text.Literal))) {
                } else
                    return false;
            }
            
            return test;
        }
        
        #endregion
    }
}
