namespace Nucleus.Plugins.Rendering {
    public interface IComponent {
        /// <summary>
        /// The Text that makes up the Component
        /// </summary>
        string Literal { get; set; }
        
        /// <summary>
        /// The character length of the <see cref="Literal"/>
        /// </summary>
        int Length { get; }
    }
}
