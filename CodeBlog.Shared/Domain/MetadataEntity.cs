using Blazor.Metadata.Domain;

namespace CodeBlog.Data.Domain
{
    [Serializable]
    public record MetadataEntity : EntityBase
    {
        public string Type { get; init; } = string.Empty;

        public MetadataEntity() { }

        private MetadataEntity(Type type)
        {
            Type = type.Name;
        }

        public static MetadataEntity FromType<T>(MetadataValues metadata)
        {
            return new MetadataEntity(typeof(T)) { Metadata = metadata};
        }
    }
    
}
