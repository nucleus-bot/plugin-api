using Nucleus.Plugins.Enums;
using Nucleus.Plugins.IO;
using Nucleus.Plugins.Rendering;

namespace Nucleus.Plugins.Contextualization {
    public interface IPluginContext : IScopedRegistryContext {
        #region Read-Only Helpers
        
        /// <summary>
        /// Filesystem helper for accessing plugin files
        /// </summary>
        IFileSystemHelper FileSystem { get; }
        
        /// <summary>
        /// A Helper class used for creating Components in the UI
        /// </summary>
        IComponentsHelper Components { get; }
        
        #endregion
        #region Events
        
        /// <summary>
        /// <para>Register a Message Middleware that is called when populating Messages to the Chat Window</para>
        /// 
        /// <para>A <paramref name="priority"/> can be set to order your Method above or below other plugins.</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description>If your plugin filters content or modifies the message you may want to be <see cref="Ordering.FIRST"/></description>
        ///     </item>
        ///     <item>
        ///         <description>If your plugin reads content or applies styling to finalized content you may want to be <see cref="Ordering.LAST"/></description>
        ///     </item>
        ///     <item>
        ///         <description>If your plugin does not care about ordering you can choose <see cref="Ordering.INSERT"/></description>
        ///     </item>
        /// </list>
        /// 
        /// <para>Methods are ordered by <see cref="Ordering.FIRST"/>, <see cref="Ordering.INSERT"/>, <see cref="Ordering.LAST"/></para>
        /// </summary>
        /// <param name="delegate">The method that will run when messages are received</param>
        /// <param name="priority">When the method should run compared to other Middleware</param>
        void MessageHandlerMiddleware(MessageReceivedDelegate @delegate, Ordering priority = Ordering.INSERT);
        
        /// <summary>
        /// Registers an asynchronous Event Listener that is called after joining a channel
        /// </summary>
        /// <param name="delegate"></param>
        void ChannelJoinEvent(ChannelStateDelegate @delegate);
        
        /// <summary>
        /// Registers an asynchronous Event Listener that is called after leaving a channel
        /// </summary>
        /// <param name="delegate"></param>
        void ChannelPartEvent(ChannelStateDelegate @delegate);
        
        /// <summary>
        /// Registers an asynchronous Event Listener that is called when receiving a channel update
        /// </summary>
        /// <param name="delegate"></param>
        void ChannelUpdateEvent(ChannelStateDelegate @delegate);
        
        #endregion
        #region Blazor/Razor
        
        void ConfigureWebServicesEvent(WebServicesDelegate @delegate);
        
        void ConfigureWebServerEvent(WebServerDelegate @delegate);
        
        #endregion
    }
}
