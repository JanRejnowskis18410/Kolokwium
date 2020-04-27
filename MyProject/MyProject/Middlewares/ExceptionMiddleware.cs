using Microsoft.AspNetCore.Http;
using MyProject.Exceptions;
using MyProject.Models;
using System;
using System.Threading.Tasks;

namespace MyProject.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            } catch(Exception exc)
            {
                await HandleExceptionAsync(context, exc);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exc)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (exc is MedicamentNotInDatabaseExecption)
            {
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    Message = exc.Message
                }.ToString());
            }

            return context.Response.WriteAsync(new ErrorDetails {
                StatusCode = (int)StatusCodes.Status500InternalServerError,
                Message = "Wystąpił jakiś błąd..."
            }.ToString());
        }
    }
}
