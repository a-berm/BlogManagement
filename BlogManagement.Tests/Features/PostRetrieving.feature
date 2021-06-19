Feature: PostRetreiving
	Retreiving posts

Scenario: Retreive all approved posts
	Given There are 4 posts in the database
	And only 2 of them are approved
	When the user wants to retrieve all the posts
	Then the application should return only 2 posts

Scenario: Retrieve a post by id
	Given There are 4 posts in the database
	And only 2 of them are approved
	When the user wants to retrieve a post whose id equals to 1
	And this post is approved
	Then the application should return this post