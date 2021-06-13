using System.Collections.Generic;
using System.Threading.Tasks;
using BlogManagement.Domain.Dto;
using BlogManagement.Domain.Entities;

namespace BlogManagement.Business.Contract
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> ListPostsAsync(long? id = null);

        Task<Post> CreatePostAsync(PostInputDto postDto);

        Task<bool> ReviewPostsAsync(long userId);
    }
}
