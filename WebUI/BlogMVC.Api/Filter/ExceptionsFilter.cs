using System.Net;
using BlogMVC.Application.Exceptions.BaseExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogMVC.Api.Filter;

public class ExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is not BlogException)
        {
            HandlerUnknowError(context);
        }
        else 
        {
            HandlerBlogExceptions(context);
        }
    }

    private void HandlerBlogExceptions(ExceptionContext context)
    {
        if(context.Exception is InvalidLoginException)
        {
            HandlerInvalidLoginException(context);
        }
    }

    private void HandlerInvalidLoginException(ExceptionContext context)
    {
        var error = context.Exception as InvalidLoginException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new ErrorResponseJson(error.Message)); 
    }
    private void HandlerNotFoundException(ExceptionContext context)
    {
        //
    }

    private void HandlerUnknowError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
        context.Result = new ObjectResult(new UnknowErrorResponseJson("Erro desconhecido"));
    }


}
