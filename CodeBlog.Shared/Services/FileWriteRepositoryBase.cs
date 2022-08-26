using CodeBlog.Data.Domain;
using CodeBlog.Data.Interfaces;
using System.Text.Json;

namespace CodeBlog.Data.Services
{
    public abstract class FileWriteRepositoryBase<T> : FileReadRepositoryBase<T>, IRepository<T>
        where T : notnull, EntityBase
    {
        #region Interface
        public virtual async Task<T> Create(T entity, CancellationToken cancellationToken = default)
        {
            await LoadFile(cancellationToken);

            entity = entity with 
            { 
                Metadata = entity.Metadata with 
                { 
                    Id = Guid.NewGuid() 
                } 
            };

            _entities.Add(entity);

            await Save(cancellationToken);

            return entity;
        }

        public virtual async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            await LoadFile(cancellationToken);
            var entity = await Read(id);
            if (_entities.Remove(entity))
            {
                await Save(cancellationToken);
                return true;
            }
            else return false;
        }

        public virtual async Task<T> Update(Guid id, T entity, CancellationToken cancellationToken = default)
        {
            await LoadFile(cancellationToken);
            var existingEntity = await Read(id);
            if (_entities.Remove(existingEntity))
            {
                existingEntity = entity with
                {
                    Metadata = entity.Metadata with
                    {
                        Id = existingEntity.Metadata.Id,
                        CreatedAt = existingEntity.Metadata.CreatedAt,
                        UpdatedAt = DateTime.Now
                    }
                };
                _entities.Add(existingEntity);
                await Save(cancellationToken);
                return entity;
            }
            else return await Read(id, cancellationToken);
        }
        #endregion Interface


        #region Helpers
        protected async Task Save(CancellationToken cancellationToken = default)
        {
            var contents = JsonSerializer.Serialize(_entities, typeof(IEnumerable<T>),
                new JsonSerializerOptions()
                {
                    AllowTrailingCommas = false,
                    WriteIndented = true,
                });
            await File.WriteAllTextAsync(FileName, contents, cancellationToken);
            await LoadFile(cancellationToken);
        }
        #endregion Helpers
    }
}
