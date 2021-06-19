using BlogManagement.Domain.Abstractions;
using BlogManagement.Domain.Enums;

namespace BlogManagement.Domain.Entities
{
    public class Administrator : User, IPostReviewer
    {
        private const int MAX_CHARACTERS = 400;

        public override Post CreatePost(string title, string content)
        {
            var post = base.CreatePost(title, content);
            post.Status = PostStatus.APPROVED;
            return post;
        }

        public void ReviewPost(Post post)
        {
            var content = post.Content;

            if (content.Length <= MAX_CHARACTERS)
                post.Status = PostStatus.APPROVED;
            else
                post.Status = PostStatus.REJECTED;
        }
    }
}
