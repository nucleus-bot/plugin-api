using System.Drawing;
using Nucleus.Plugins.Rendering;

namespace Nucleus.Plugins.Contextualization {
    public interface IChannelContext : IScopedRegistryContext, IUserContext {
        /// <summary>
        /// The Channels color branding
        /// </summary>
        Color Color { get; set; }
    }
}
