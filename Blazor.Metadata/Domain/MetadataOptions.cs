using System.Collections.Immutable;

namespace Blazor.Metadata.Domain
{
    public class MetadataOptions
    {
        public IDictionary<string, MetadataValues> Metadatas { get; set; } = ImmutableDictionary<string, MetadataValues>.Empty;
    }
}
