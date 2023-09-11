using Nucleus.Plugins.Contextualization;

namespace Nucleus.Plugins {
    /// <summary>
    /// All plugins must implement this interface in order to be loaded
    /// </summary>
    public interface IPlugin {
        /// <summary>
        /// Registration of all Program modifications and interaction should be done here
        /// Initializing of objects and other things can be done in the Plugin Constructor
        /// </summary>
        ValueTask RegisterAsync(IPluginContext context);
    }
}
