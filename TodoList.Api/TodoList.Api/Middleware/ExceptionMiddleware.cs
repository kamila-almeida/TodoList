using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using TodoList.Application.Models;
using TodoList.Domain.Enums;

namespace TodoList.Api.Middleware
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, bool internalServerErrorWithException)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var exception = contextFeature.Error.InnerException ?? contextFeature.Error;


                    var operationResult = new BaseModel<Exception>(false, EMessages.InternalError, (internalServerErrorWithException ? exception : null));

                    string json = JsonConvert.SerializeObject(operationResult,
                        new JsonSerializerSettings
                        {
                            ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                            Formatting = Formatting.Indented,
                            NullValueHandling = NullValueHandling.Ignore
                        });

                    await context.Response.WriteAsync(json);
                });
            });
        }
    }
}
