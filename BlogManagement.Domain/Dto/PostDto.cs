using System;

namespace BlogManagement.Domain.Dto
{
    public class PostDto
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string EditionDate { get; set; }
    }
}
