using Blazor.Metadata.Domain;

namespace CodeBlog.Data.Domain
{
    public abstract record EntityBase
    {
        public MetadataValues Metadata { get; init; } = default!;
    }
}
