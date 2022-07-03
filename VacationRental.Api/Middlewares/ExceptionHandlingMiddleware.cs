using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace VacationRental.Api.Middlewares
{
    /// <summary>
    /// Exception Handling Middleware.
    /// </summary>
    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        /// <summary>
        /// It's been invoked for each exception handling.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
