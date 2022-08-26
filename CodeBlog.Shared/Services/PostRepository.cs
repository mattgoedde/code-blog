using CodeBlog.Data.Domain;
using CodeBlog.Data.Interfaces;

namespace CodeBlog.Data.Services
{
    public class PostRepository : FileWriteRepositoryBase<Post>
    {
        private readonly IRepository<MetadataEntity> _metaRepo;
        public PostRepository(IRepository<MetadataEntity> metaRepo)
        {
            FileName = "posts.json";
            _metaRepo = metaRepo;
        }

        public override async Task<Post> Create(Post entity, CancellationToken cancellationToken = default!)
        {
            var newPost = await base.Create(entity, cancellationToken);
            await _metaRepo.Create(MetadataEntity.FromType<Post>(newPost.Metadata), cancellationToken);
            return newPost;
        }

        public override async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default!)
        {
            await _metaRepo.Delete(id, cancellationToken);
            return await base.Delete(id, cancellationToken);
        }

        public override async Task<Post> Update(Guid id, Post entity, CancellationToken cancellationToken = default!)
        {
            var updatedPost = await base.Update(id, entity, cancellationToken);
            await _metaRepo.Update(id, MetadataEntity.FromType<Post>(updatedPost.Metadata), cancellationToken);
            return updatedPost;
        }
    }
}
