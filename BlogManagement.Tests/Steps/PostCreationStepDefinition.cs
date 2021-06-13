using BlogManagement.Business;
using BlogManagement.Business.Contract;
using BlogManagement.Domain.Dto;
using BlogManagement.Domain.Entities;
using BlogManagement.Persistance.Contract;
using BlogManagement.Persistance.Utils;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;
using NSubstitute.ReturnsExtensions;
using BlogManagement.Domain.Exceptions;
using System.Threading.Tasks;

namespace BlogManagement.Tests.Steps
{
    [Binding]
    public sealed class PostCreationStepDefinition
    {
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        RegularUser regularUser;
        Administrator administrator;
        Post post;
        Post createdPost;
        PostInputDto postInput;

        public PostCreationStepDefinition(ScenarioContext scenarioContext)
        {
            _postRepository = Substitute.For<IPostRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _postService = new PostService(_postRepository, _userRepository);
        }

        [Given(@"A regular user who wants to create a post")]
        public void GivenARegularUserWhoWantsToCreateAPost()
        {
            regularUser = new RegularUser();
        }

        [When(@"a post is created by this regular user")]
        public void APostIsCreatedByThisRegularUser()
        {
            post = regularUser.CreatePost("title", "content");
        }

        [Given(@"An administrator who wants to create a post")]
        public void GivenAnAdministratorWhoWantsToCreateAPost()
        {
            administrator = new Administrator();
        }

        [When(@"a post is created by this administrator")]
        public void APostIsCreatedByThisAdministrator()
        {
            post = administrator.CreatePost("title", "content");
        }

        [Then(@"the post status is (.*)")]
        public void ThenThePostStatusIs(string status)
        {
            Assert.Equal(status, post.Status.ToString());
        }

        [Given(@"An existing user who wants to create a post")]
        public void GivenAnExistingUserWhoWantsToCreateAPost()
        {
            var existingUser = GenerateUser();
            var createdPost = GeneratePost();
            _userRepository.GetUserByIdAsync(Arg.Any<long>()).ReturnsForAnyArgs(existingUser);
            _postRepository.SavePostAsync(Arg.Any<Post>()).ReturnsForAnyArgs(createdPost);
        }

        [Given(@"An unexisting user who wants to create a post")]
        public void GivenAnUnexistingUserWhoWantsToCreateAPost()
        {
            _userRepository.GetUserByIdAsync(Arg.Any<long>()).ReturnsNull();
        }

        [When(@"this user requests a post creation")]
        public void WhenThisUserRequestsAPostCreation()
        {
            postInput = new PostInputDto() { UserId = 1 };
        }

        [Then(@"he will be able to create the post")]
        public async void ThenHeWillBeAbleToCreateThePost()
        {
            createdPost = await _postService.CreatePostAsync(postInput);
            Assert.NotNull(createdPost);
        }

        [Then(@"he will not be able to create the post")]
        public async Task ThenHeWillNotBeAbleToCreateThePost()
        {
            await Assert.ThrowsAsync<UserNotFoundException>(() => _postService.CreatePostAsync(postInput));
        }

        private Administrator GenerateUser()
        {
            List<Administrator> administrators = new List<Administrator>();
            DataBaseOperations.LoadEntities("Data\\administrators.json", administrators);
            return administrators.FirstOrDefault();
        }

        private Post GeneratePost()
        {
            List<Post> posts = new List<Post>();
            DataBaseOperations.LoadEntities("Data\\posts.json", posts);
            return posts.FirstOrDefault();
        }
    }
}
