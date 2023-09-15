namespace Nucleus.Plugins.Razor {
    public interface IServerHelper {
        /// <summary>
        /// Get the <see cref="Uri"/> for a specific Razor Page
        /// </summary>
        /// <param name="variables"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Uri? GetPath<T>(params object[] variables);
    }
}
