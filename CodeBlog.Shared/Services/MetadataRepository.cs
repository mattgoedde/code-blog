using Blazor.Metadata.Domain;
using Blazor.Metadata.Interfaces;
using CodeBlog.Data.Domain;

namespace CodeBlog.Data.Services
{
    public class MetadataRepository : FileWriteRepositoryBase<MetadataEntity>, IRouteMetadataProvider
    {
        public MetadataRepository()
        {
            FileName = "meta.json";
        }

        public override async Task<MetadataEntity> Create(MetadataEntity entity, CancellationToken cancellationToken = default!)
        {
            await LoadFile(cancellationToken);
            _entities.Add(entity);

            await Save(cancellationToken);

            return entity;
        }

        public async Task<IDictionary<string, MetadataValues>> GetRouteMetadataAsync(CancellationToken cancellationToken = default)
        {
            var metas = await Read(cancellationToken);

            return metas.ToDictionary(m => $"{m.Type}/{m.Metadata.Id}", m => m.Metadata);
        }
    }
}
