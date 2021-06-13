using BlogManagement.Domain.Entities;
using BlogManagement.Persistance.Utils;
using System.Collections.Generic;

namespace BlogManagement.Persistance.DataBase
{
    public class FakeDataBase : IDataBase
    {
        public List<User> Users { get; set; } = new List<User>();

        public List<Post> Posts { get; set; } = new List<Post>();

        public FakeDataBase()
        {
            LoadUsers();
            LoadPosts();
        }

        private void LoadUsers()
        {
            List<Administrator> administrators = new List<Administrator>();

            List<RegularUser> regularUsers = new List<RegularUser>();

            DataBaseOperations.LoadEntities("Data\\administrators.json", administrators);

            DataBaseOperations.LoadEntities("Data\\regularUsers.json", regularUsers);

            Users.AddRange(administrators);
            Users.AddRange(regularUsers); 
        }

        private void LoadPosts()
        {
            DataBaseOperations.LoadEntities("Data\\posts.json", Posts);
        }
    }
}
