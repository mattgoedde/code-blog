namespace CodeBlog.Data.Interfaces
{
    public interface IRepository<T> : IReadRepository<T>
    {
        Task<T> Create(T entity, CancellationToken cancellationToken = default);
        Task<T> Update(Guid id, T entity, CancellationToken cancellationToken = default);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken = default);
    }
}