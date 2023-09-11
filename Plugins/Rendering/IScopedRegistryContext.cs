using System.Net.Http.Headers;

namespace Nucleus.Plugins.Rendering {
    public interface IScopedRegistryContext {
        #region Registries
        
        /// <summary>
        /// Read a URL for Command Registration
        /// </summary>
        /// <param name="uri">The URL to read NucleusBot command JSON from</param>
        /// <param name="headers">Consumer action that is made before to the Request, allows for setting Http Headers</param>
        IDisposable RegisterCommands(Uri uri, Action<HttpRequestHeaders>? headers = null);
        
        /// <summary>
        /// Read a Command JSON file for Command Registration
        /// </summary>
        /// <param name="path">A path to a JSON file that can be read for Commands</param>
        /// <exception cref="ArgumentException">If the Path is absolute or leaves the Plugin folder</exception>
        IDisposable RegisterCommands(string path);
        
        /// <summary>
        /// Read a URL for Emote Registration
        /// </summary>
        /// <param name="uri">The URL to read NucleusBot emote data from</param>
        /// <param name="headers">Consumer action that is made before to the Request, allows for setting Http Headers</param>
        /// <param name="converter">Create a set of Emotes from the <see cref="HttpResponseMessage"/></param>
        IDisposable RegisterEmotes(Uri uri, Action<HttpRequestHeaders>? headers = null, EnumerableDelegate<Emote, string>? converter = null);
        
        /// <summary>
        /// Read a Command JSON file for Emote Registration
        /// </summary>
        /// <param name="path">A path to a JSON file that can be read for Emotes</param>
        /// <param name="converter">Create a set of Emotes from the <see cref="HttpResponseMessage"/></param>
        /// <exception cref="ArgumentException">If the Path is absolute or leaves the Plugin folder</exception>
        IDisposable RegisterEmotes(string path, EnumerableDelegate<Emote, string>? converter = null);
        
        #endregion
    }
}
