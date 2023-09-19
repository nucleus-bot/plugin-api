namespace Nucleus.Api.Objects {
    /// <summary>
    /// An <see cref="IResource"/> that may exist, but may only be an uninitialized <see cref="VagueResource{T}.UUID"/>.
    /// Usable as a Method parameter that can accept either or, but will eventually need to try loading the <see cref="VagueResource{T}.Resource"/> from somewhere
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class VagueResource<T> : IResource where T : IResource {
        /// <summary>
        /// The UUID of the Resource <see cref="T"/>
        /// </summary>
        public Guid UUID { get; init; }
        
        /// <inheritdoc />
        Guid? IResource.UUID => this.UUID;
        
        /// <summary>
        /// The resource if it is defined
        /// </summary>
        public T? Resource {
            get => object.Equals(this.UUID, this._Resource?.UUID) ? this._Resource : default;
            set => this._Resource = value;
        }
        private T? _Resource;
        
        public static implicit operator VagueResource<T>(T value)
            => new()  { UUID = value.UUID ?? Guid.Empty, Resource = value };
        
        public static implicit operator VagueResource<T>(Guid guid)
            => new() { UUID = guid };
        
        public static implicit operator Guid(VagueResource<T> resource)
            => resource.UUID;
        
        public static explicit operator T?(VagueResource<T> resource)
            => resource.Resource;
    }
}
