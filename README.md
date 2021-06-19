# BlogManagement
A simple web api that allows users to perform operations on a blog.

## Description

This web api handles operation on blog posts.
* A user is either a regular user or an administrator.
* When a regular user creates a post, this post should be reviewed. 
* When an administrator creates a post, this post is auto-approved.
* Only approved posts are visible to users.
* Only administrators can review posts.

## Guidelines
* When the application is started, some users and posts are automatically loaded.
* BDD Tests have been generated using SpecFlow.
* Exceptions at the controller level, are handled using an exception filter.
* Depending on the route, input data is controlled manually or automatically.

## Getting Started

Launch the application using IIS, and it will automatically redirect you to swagger UI.

### Installing

SpecFlow For Visual Studio extension should be installed.

## Built With
* C#, ASP.net Core (2.1), Swagger.
* xUnit with SpecFlow and NSubstitute.
* Visual Studio 2017.
