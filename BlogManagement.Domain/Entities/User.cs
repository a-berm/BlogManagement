namespace BlogManagement.Domain.Entities
{
    public abstract class User : ILoadable
    {
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public virtual Post CreatePost(string title, string content)
        {
            return new Post(title, content, UserId, NickName);
        }
    }
}
