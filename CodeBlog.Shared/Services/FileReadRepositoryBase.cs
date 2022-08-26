using CodeBlog.Data.Domain;
using CodeBlog.Data.Interfaces;
using System.Text.Json;

namespace CodeBlog.Data.Services
{
    public abstract class FileReadRepositoryBase<T> : IReadRepository<T>
        where T : notnull, EntityBase
    {
        protected IList<T> _entities = Enumerable.Empty<T>().ToList();
        protected string FileName = "data.json";

        #region Interface
        public virtual async Task<T> Read(Guid id, CancellationToken cancellationToken = default)
        {
            await LoadFile(cancellationToken);
            return _entities.Single(p => p.Metadata.Id == id);
        }

        public virtual async Task<IEnumerable<T>> Read(CancellationToken cancellationToken = default)
        {
            await LoadFile(cancellationToken);
            return _entities.ToList();
        }

        public virtual async Task<IEnumerable<T>> Read(CancellationToken cancellationToken = default, params Guid[] ids)
        {
            await LoadFile(cancellationToken);
            return _entities.Where(p => ids.Contains(p.Metadata.Id)).ToList();
        }
        #endregion Interface

        #region Helpers
        protected async Task LoadFile(CancellationToken cancellationToken = default)
        {
            using FileStream stream = File.OpenRead(FileName);
            _entities = (await JsonSerializer.DeserializeAsync<IEnumerable<T>>(stream, cancellationToken: cancellationToken) ?? Enumerable.Empty<T>()).ToList();
            stream.Close();
        }
        #endregion Helpers
    }
}
