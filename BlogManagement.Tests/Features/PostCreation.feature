Feature: PostCreation
	Post Creation

Scenario: Post creation by a regular user
	Given A regular user who wants to create a post
	When a post is created by this regular user
	Then the post status is PENDING

Scenario: Post creation by an administrator
	Given An administrator who wants to create a post
	When a post is created by this administrator
	Then the post status is APPROUVED

Scenario: Post creation by an existing user
	Given An existing user who wants to create a post
	When this user requests a post creation
	Then he will be able to create the post

Scenario: Post creation by an unexisting user
	Given An unexisting user who wants to create a post
	When this user requests a post creation
	Then he will not be able to create the post