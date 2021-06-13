using AutoMapper;
using BlogManagement.Business.Contract;
using BlogManagement.Domain.Dto;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Enums;
using BlogManagement.Domain.Exceptions;
using BlogManagement.Persistance.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.Business
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<PostDto>> ListPostsAsync(long? id = null)
        {
            try
            {
                var posts = await _postRepository.GetPostsAsync(id);
                var approuvedPosts = posts.Where(p => p.Status == PostStatus.APPROUVED);
                var postsDto = Mapper.Map<IEnumerable<PostDto>>(approuvedPosts);
                return postsDto;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<Post> CreatePostAsync(PostInputDto postDto)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(postDto.UserId);

                if (user == null)
                {
                    throw new UserNotFoundException(postDto.UserId);
                }

                var post = user.CreatePost(postDto.Title, postDto.Content);

                var createdPost = await _postRepository.SavePostAsync(post);

                return createdPost;
            }
            catch (UserNotFoundException userNotFoundException)
            {
                throw userNotFoundException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<bool> ReviewPostsAsync(long userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);

                if (user == null)
                {
                    throw new UserNotFoundException(userId);
                }

                if (!(user is Administrator))
                {
                    throw new OperationDeniedException(userId);
                }

                var posts = await _postRepository.GetUnreviewedPostsAsync();

                if (posts == null || !posts.Any())
                {
                    throw new PostsAlreadyReviewedException();
                }

                var administrator = user as Administrator;

                posts.ForEach(post => administrator.ReviewPost(post));

                await _postRepository.UpdatePostsAsync(posts);

                return true;
            }
            catch (UserNotFoundException userNotFoundException)
            {
                throw userNotFoundException;
            }
            catch (OperationDeniedException operationDeniedException)
            {
                throw operationDeniedException;
            }
            catch (PostsAlreadyReviewedException postsAlreadyReviewedException)
            {
                throw postsAlreadyReviewedException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
