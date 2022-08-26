namespace CodeBlog.Data.Interfaces
{
    public interface IReadRepository<T>
    {
        Task<T> Read(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> Read(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> Read(CancellationToken cancellationToken = default, params Guid[] ids);
    }
}
