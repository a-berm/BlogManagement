using BlogManagement.Domain.Enums;
using System;

namespace BlogManagement.Domain.Entities
{
    public class Post : ILoadable 
    {
        public long PostId { get; set; }

        public long UserId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime EditionDate { get; set; }

        public PostStatus Status { get; set; }

        public Post(string title, string content, long userId, string author)
        {
            Title = title;
            Content = content;
            UserId = userId;
            Author = author;
            EditionDate = DateTime.Now;
        }
    }
}
