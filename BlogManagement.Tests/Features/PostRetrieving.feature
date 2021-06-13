Feature: PostRetreiving
	Retreiving posts

Scenario: Retreive all approuved posts
	Given There are 4 posts in the database
	And only 2 of them are approuved
	When the user wants to retrieve all the posts
	Then the application should return only 2 posts

Scenario: Retrieve a post by id
	Given There are 4 posts in the database
	And only 2 of them are approuved
	When the user wants to retrieve a post whose id equals to 1
	And this post is approuved
	Then the application should return this post