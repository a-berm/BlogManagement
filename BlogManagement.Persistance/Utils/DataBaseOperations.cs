using BlogManagement.Domain.Abstractions;
using BlogManagement.Domain.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BlogManagement.Persistance.Utils
{
    public static class DataBaseOperations
    {
        public static void SetPostId(Post post, List<Post> postsInDataBase)
        {
            var newId = postsInDataBase.Max(p => p.PostId) + 1;
            post.PostId = newId;
        }

        public static void UpdatePosts(List<Post> posts, List<Post> postsInDataBase)
        {
            foreach (var post in posts)
            {
                var index = postsInDataBase.FindIndex(p => p.PostId == post.PostId);

                if (index != -1)
                    postsInDataBase[index] = post;
            }
        }

        public static void LoadEntities<T>(string fileName, List<T> dbSet) where T : ILoadable
        {
            var executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var path = Path.Combine(executableLocation, fileName);

            var entities = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path)).ToList();

            dbSet.AddRange(entities);
        }
    }
}
