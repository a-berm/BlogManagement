using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Enums;
using BlogManagement.Persistance.Contract;
using BlogManagement.Persistance.DataBase;
using BlogManagement.Persistance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.Persistance
{
    public class PostRepository : IPostRepository
    {
        private readonly IDataBase _dataBase;

        public PostRepository(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }

        public async Task<List<Post>> GetPostsAsync(long? postId)
        {
            try
            {
                var posts = _dataBase.Posts;

                if (postId != null)
                    posts = posts.Where(p => p.PostId == postId).ToList();

                return await Task.FromResult(posts);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<List<Post>> GetUnreviewedPostsAsync()
        {
            try
            {
                var unreviewedPosts = _dataBase.Posts.Where(p => p.Status == PostStatus.PENDING).ToList();
                return await Task.FromResult(unreviewedPosts);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<Post> SavePostAsync(Post post)
        {
            try
            {
                var postsInDataBase = _dataBase.Posts;
                DataBaseOperations.SetPostId(post, postsInDataBase);
                _dataBase.Posts.Add(post);
                return await Task.FromResult(post);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task UpdatePostsAsync(List<Post> posts)
        {
            try
            {
                var postsInDataBase = _dataBase.Posts;
                DataBaseOperations.UpdatePosts(posts, postsInDataBase);
                await Task.CompletedTask;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
