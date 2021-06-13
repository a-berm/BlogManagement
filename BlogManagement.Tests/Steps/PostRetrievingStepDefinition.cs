using AutoMapper;
using BlogManagement.Business;
using BlogManagement.Business.AutoMapper;
using BlogManagement.Business.Contract;
using BlogManagement.Domain.Dto;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Enums;
using BlogManagement.Persistance.Contract;
using BlogManagement.Persistance.Utils;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Xunit;

namespace BlogManagement.Tests.Steps
{
    [Binding]
    public sealed class PostRetrievingStepDefinition
    {
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        List<PostDto> result = new List<PostDto>();

        Post specificPost;

        List<Post> postsInDb = new List<Post>();


        public PostRetrievingStepDefinition(ScenarioContext scenarioContext)
        {
            _postRepository = Substitute.For<IPostRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _postService = new PostService(_postRepository, _userRepository);
            Mapper.Initialize(cfg => cfg.AddProfile<BlogManagementMapperProfile>());
        }

        [Given(@"There are (.*) posts in the database")]
        public void GivenThereArePostsInTheDatabase(int nbrOfPostsInDb)
        {
            postsInDb = GeneratePosts();
            Assert.Equal(nbrOfPostsInDb, postsInDb.Count);
        }

        [Given(@"only (.*) of them are approuved")]
        public void GivenOnlyOfThemAreApprouved(int nbrOfApprouvedPostsInDb)
        {
            var approuvedPosts = postsInDb.Where(p => p.Status == PostStatus.APPROUVED).ToList();
            Assert.Equal(nbrOfApprouvedPostsInDb, approuvedPosts.Count);
        }

        [When(@"the user wants to retrieve all the posts")]
        public async void WhenTheUserWantsToRetreiveAllThePosts()
        {
            _postRepository.GetPostsAsync(null).ReturnsForAnyArgs(postsInDb);
            var posts = await _postService.ListPostsAsync();
            result = posts.ToList();
        }

        [Then(@"the application should return only (.*) posts")]
        public void ThenTheApplicationShouldReturnOnlyApprouvedPosts(int nbrOfApprouvedPosts)
        {
            Assert.Equal(nbrOfApprouvedPosts, result.Count);
        }

        [When(@"the user wants to retrieve a post whose id equals to (.*)")]
        public async void WhenTheUserWantsToRetrieveAPostWhoseIdEqualsTo(int postId)
        {
            var listWithSpecificPost = postsInDb.Where(p => p.PostId == postId).ToList();
            _postRepository.GetPostsAsync(postId).ReturnsForAnyArgs(listWithSpecificPost);
            specificPost = listWithSpecificPost.FirstOrDefault();
            var posts = await _postService.ListPostsAsync();
            result = posts.ToList();
        }

        [When(@"this post is approuved")]
        public void WhenThisPostIsApprouved()
        {
            Assert.True(specificPost.Status == PostStatus.APPROUVED);
        }

        [Then(@"the application should return this post")]
        public void ThenTheApplicationShouldReturnThisPostWithId()
        {
            Assert.Single(result);
        }

        private List<Post> GeneratePosts()
        {
            List<Post> posts = new List<Post>();
            DataBaseOperations.LoadEntities("Data\\posts.json", posts);
            return posts;
        }
    }
}
