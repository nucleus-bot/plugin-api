namespace Nucleus.Plugins.Contextualization {
    public interface IUserContext {
        /// <summary>
        /// 
        /// </summary>
        Guid UUID { get; }
        
        /// <summary>
        /// 
        /// </summary>
        string Id { get; }
        
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }
    }
}
