using BlogManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace BlogManagement.Domain.ExceptionFilter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ActionExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            if (context.Exception is UserNotFoundException || context.Exception is OperationDeniedException
                || context.Exception is PostsAlreadyReviewedException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int) statusCode;
            context.Result = new ObjectResult(context.Exception.Message);
        }
    }
}
