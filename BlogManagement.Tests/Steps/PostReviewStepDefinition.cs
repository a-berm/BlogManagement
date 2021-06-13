using BlogManagement.Business;
using BlogManagement.Business.Abstractions;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Enums;
using BlogManagement.Domain.Exceptions;
using BlogManagement.Persistance.Abstractions;
using BlogManagement.Persistance.Utils;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace BlogManagement.Tests.Steps
{
    [Binding]
    public sealed class PostReviewStepDefinition
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        RegularUser regularUser;
        Administrator administrator;
        List<Post> pendingPosts;
        Post post;
        bool postsReviewed;

        public PostReviewStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _postRepository = Substitute.For<IPostRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _postService = new PostService(_postRepository, _userRepository);
        }

        [Given(@"An admnistrator who wants to review a post")]
        public void GivenAnAdmnistratorWhoWantsToReviewAPost()
        {
            administrator = new Administrator();
        }

        [When(@"the maximun number of characters of the content is lower than (.*)")]
        public void WhenTheMaximunNumberOfCharactersOfTheContentIsLowerThan(int maxCharacters)
        {
            post = GeneratePosts(4).FirstOrDefault();
        }

        [When(@"the maximun number of characters of the content is greater than (.*)")]
        public void WhenTheMaximunNumberOfCharactersOfTheContentIsGreaterThan(int maxCharacters)
        {
            post = GeneratePosts(3).FirstOrDefault();
        }

        [Then(@"the post is (.*)")]
        public void ThenThePostIs(string status)
        {
            administrator.ReviewPost(post);
            Assert.Equal(post.Status.ToString(), status);
        }

        [Given(@"An existing user who wants to review all posts")]
        public void GivenAnExistingUserWhoWantsToReviewAllPosts()
        {
            administrator = GenerateAdministrator();
        }


        [Given(@"this user is an administator")]
        public void GivenThisUserIsAnAdministator()
        {
            Assert.True(administrator is Administrator);
            _userRepository.GetUserByIdAsync(1).Returns(administrator);
        }

        [Given(@"there are pending posts")]
        public void GivenThereArePendingPosts()
        {
            pendingPosts = GeneratePosts(4);
            pendingPosts.ForEach(p => Assert.Equal(PostStatus.PENDING, p.Status));
            _postRepository.GetUnreviewedPostsAsync().ReturnsForAnyArgs(pendingPosts);
        }

        [When(@"this user requests all posts review")]
        public async void WhenThisUserRequestsAllPostsReview()
        {

            postsReviewed = await _postService.ReviewPostsAsync(1);
        }

        [Then(@"pending posts are reviewed")]
        public void ThenPendingPostsAreReviewed()
        {
            Assert.True(postsReviewed);
        }

        [Given(@"An unexisting user who wants to review all posts")]
        public void GivenAnUnexistingUserWhoWantsToReviewAllPosts()
        {
            _userRepository.GetUserByIdAsync(Arg.Any<long>()).ReturnsNull();
        }

        [Then(@"he will not be able to perform the operation")]
        public async Task ThenHeWillNotBeAbleToPerformTheOperation()
        {
            var testName = _scenarioContext.ScenarioInfo.Title;
            if (testName.Equals("Post review by an unexisting user"))
                await Assert.ThrowsAsync<UserNotFoundException>(() => _postService.ReviewPostsAsync(Arg.Any<long>()));
            if (testName.Equals("Post review by a regular user"))
                await Assert.ThrowsAsync<OperationDeniedException>(() => _postService.ReviewPostsAsync(2));
            if (testName.Equals("Posts are already reviewed"))
                await Assert.ThrowsAsync<PostsAlreadyReviewedException>(() => _postService.ReviewPostsAsync(1));
        }

        [Given(@"A regular user who wants to review all posts")]
        public void GivenARegularUserWhoWantsToReviewAllPosts()
        {
            regularUser = GenerateRegularUser();
            Assert.True(regularUser is RegularUser);
            _userRepository.GetUserByIdAsync(2).Returns(regularUser);
        }


        [Given(@"posts are already reviewed")]
        public void GivenPostsAreAlreadyReviewed()
        {
            _postRepository.GetUnreviewedPostsAsync().ReturnsNull();
        }

        private Administrator GenerateAdministrator()
        {
            List<Administrator> administrators = new List<Administrator>();
            DataBaseOperations.LoadEntities("Data\\administrators.json", administrators);
            return administrators.FirstOrDefault();
        }

        private RegularUser GenerateRegularUser()
        {
            List<RegularUser> regularUsers = new List<RegularUser>();
            DataBaseOperations.LoadEntities("Data\\regularUsers.json", regularUsers);
            return regularUsers.FirstOrDefault();
        }

        private List<Post> GeneratePosts(long? id)
        {
            List<Post> posts = new List<Post>();
            DataBaseOperations.LoadEntities("Data\\posts.json", posts);
            if(id != null)
                posts = posts.Where(p => p.PostId == id).ToList();
            return posts;
        }
    }
}
