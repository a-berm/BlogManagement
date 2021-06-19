Feature: PostReview
	Post Review

Scenario: Post review rules
	Given An admnistrator who wants to review a post
	When the maximun number of characters of the content is lower than 400
	Then the post is APPROVED
	When the maximun number of characters of the content is greater than 400
	Then the post is REJECTED

Scenario: Post review by an administrator
	Given An existing user who wants to review all posts
	And this user is an administator
	And there are pending posts
	When this user requests all posts review 
	Then pending posts are reviewed

Scenario: Post review by an unexisting user
	Given An unexisting user who wants to review all posts
	Then he will not be able to perform the operation

Scenario: Post review by a regular user
	Given A regular user who wants to review all posts
	Then he will not be able to perform the operation

Scenario: Posts are already reviewed
	Given An existing user who wants to review all posts
	And this user is an administator
	And posts are already reviewed
	Then he will not be able to perform the operation