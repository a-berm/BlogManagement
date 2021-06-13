using BlogManagement.Domain.Enums;

namespace BlogManagement.Domain.Entities
{
    public class RegularUser : User
    {
        public override Post CreatePost(string title, string content)
        {
            var post = base.CreatePost(title, content);
            post.Status = PostStatus.PENDING;
            return post;
        }
    }
}
