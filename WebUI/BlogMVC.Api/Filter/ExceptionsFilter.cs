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
    }
    private void HandlerUnknowError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
        context.Result = new ObjectResult(new UnknowErrorResponseJson("Erro desconhecido"));
    }
}
