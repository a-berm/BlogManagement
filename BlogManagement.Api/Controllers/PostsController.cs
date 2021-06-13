using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Business.Abstractions;
using BlogManagement.Domain.Dto;
using BlogManagement.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postCreationService)
        {
            _postService = postCreationService;
        }

        /// <summary>
        /// Lists all approuved posts.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PostDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PostDto>>> ListPosts()
        {
            var posts = await _postService.ListPostsAsync();

            if (!posts.Any())
                return NotFound($"No post found !");

            return Ok(posts);
        }

        /// <summary>
        /// Gets an approuved post by id.
        /// </summary>
        /// <param name="postId">The post id to search</param>
        [HttpGet("{postId:long}")]
        [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PostDto>> GetPost(long postId)
        {
            if (postId <= 0)
                return BadRequest("userId should be greather than 0 !");

            var posts = await _postService.ListPostsAsync(postId);

            var post = posts.FirstOrDefault();

            if (post == null)
                return NotFound($"No post found with postId : {postId} !");

            return Ok(post);
        }

        /// <summary>
        /// Creates a post.
        /// </summary>
        /// <param name="postDto">The post content with the user id of the post author</param>
        [HttpPost]
        [ProducesResponseType(typeof(Post), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Post>> CreatePost(PostInputDto postDto)
        {
            var post = await _postService.CreatePostAsync(postDto);
            return Created(new Uri(Request.Host + Request.Path + "/" + post.PostId), post);
        }

        /// <summary>
        /// Reviews all pending posts.
        /// </summary>
        /// <param name="userId">The user id of posts reviewer</param>
        [HttpPost("{userId:long}/review")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ReviewPosts(long userId)
        {
            if (userId <= 0)
                return BadRequest("userId should be greather than 0 !");

            await _postService.ReviewPostsAsync(userId);

            return NoContent();
        }
    }
}
