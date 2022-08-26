using Blazor.Metadata.Domain;

namespace Blazor.Metadata.Interfaces
{
    public interface IRouteMetadataProvider
    {
        Task<IDictionary<string, MetadataValues>> GetRouteMetadataAsync(CancellationToken cancellationToken = default);
    }
}
