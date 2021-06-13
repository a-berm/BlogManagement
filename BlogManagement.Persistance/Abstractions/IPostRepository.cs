using BlogManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Persistance.Abstractions
{
    public interface IPostRepository
    {
        Task<List<Post>> GetPostsAsync(long? postId);

        Task<Post> SavePostAsync(Post post);

        Task<List<Post>> GetUnreviewedPostsAsync();

        Task UpdatePostsAsync(List<Post> posts);
    }
}
