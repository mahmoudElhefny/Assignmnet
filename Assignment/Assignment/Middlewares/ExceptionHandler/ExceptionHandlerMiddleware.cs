using Assignment.Domain.ViewModels;
using Assignment.Service.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace Assignment.Middlewares.ExceptionHandler
{
    public static class ExceptionHandlerMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(configure =>
            {
                configure.Run(async context =>
                {
                    Response<Empty> generalResponse = new Response<Empty>();
                    generalResponse.IsSucceded = false;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>().Error;
                    //Set statusCode
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    //Set contentType
                    context.Response.ContentType = "application/json";

                    if (contextFeature is BaseException)
                    {
                        var baseException = (BaseException)contextFeature;

                        //Copy responseMessage to response
                        generalResponse.Errors = ((BaseException)contextFeature).Errors;
                    }

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(generalResponse));
                });
            });
        }
    }
}
