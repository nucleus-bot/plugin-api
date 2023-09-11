using Nucleus.Plugins.Contextualization;

namespace Nucleus.Plugins {
    /// <summary>
    /// This class acts as a Base for a Plugin, it implements <see cref="IPlugin"/> Methods so that not every plugin needs to clutter itself by implementing every Method
    /// </summary>
    public abstract class PluginBase : IPlugin {
        /// <inheritdoc />
        public virtual async ValueTask RegisterAsync(IPluginContext context)
            => await Task.Run(() => this.Register(context));
        
        protected virtual void Register(IPluginContext context) {}
    }
}
