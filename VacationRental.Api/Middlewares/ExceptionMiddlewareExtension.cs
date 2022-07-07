using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;

namespace VacationRental.Api.Middlewares
{
    /// <summary>
    /// Exception Handling.
    /// </summary>
    public static class ExceptionMiddlewareExtension
    {
        /// <summary>
        /// Configure the Exception Handler.
        /// </summary>
        /// <param name="app"></param>
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature == null)
                        return;

                    // TODO: Use can logger the error using the "contextFeature.Error".

                    // The object that contains information about the response.
                    var errorDetails = new ApplicationException(contextFeature.Error?.Message);

                    var jsonObject = JsonConvert.SerializeObject(errorDetails);

                    // Microsoft.AspNetCore.Http.Abstractions
                    await context.Response.WriteAsync(jsonObject);
                });
            });
        }
    }
}
