using BlogManagement.Domain.Entities;
using System.Collections.Generic;

namespace BlogManagement.Persistance.DataBase
{
    public interface IDataBase
    {
        List<User> Users { get; set; }

        List<Post> Posts { get; set; }
    }
}
