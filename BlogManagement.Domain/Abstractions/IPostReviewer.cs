using BlogManagement.Domain.Entities;

namespace BlogManagement.Domain.Abstractions
{
    public interface IPostReviewer
    {
        void ReviewPost(Post post);
    }
}
