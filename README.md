# BlogManagement
A simple web api that allows users to perform operations on a blog.

## Description

This web api handles operation on blog posts.
* A user is either a regular user or an administrator.
* When a regular user creates a post, this post should be reviewed. 
* When an administrator creates a post, this post is auto-approuved.
* Only approuved posts are visible to users.
* Only administrators can review posts.

## Guidelines
* When the application is started, some users and posts are automatically loaded.
* BDD Tests have been created using SpecFlow.

## Getting Started

Launch the application using IIS, and it will automatically redirect you to swagger UI.

### Installing

SpecFlow For Visual Studio extension should be installed.

## Built With
* C#, ASP.net Core (2.1), Swagger.
* xUnit with SpecFlow and NSubstitute.

