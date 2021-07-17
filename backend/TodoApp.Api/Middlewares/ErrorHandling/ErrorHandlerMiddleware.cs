using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Dynamic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TodoApp.Domain.Exceptions;

namespace TodoApp.Api.Middlewares.ErrorHandling
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlerMiddleware> logger;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private readonly IHostEnvironment hostEnvironment;
        private const string MessageTemplate = "Unhandled {Exception} caught.";

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger, IHostEnvironment hostEnvironment)
        {
            this.next = next;
            this.logger = logger;
            this.hostEnvironment = hostEnvironment;
            this.jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (EntityNotFoundException e)
            {
                logger.LogError(e, MessageTemplate, e.GetType().Name);

                await WriteResponseAsJsonAsync(
                    context,
                    HttpStatusCode.NotFound,
                    new ErrorResponse
                    {
                        Title = e.Message,
                        StackTrace = e.StackTrace
                    });
            }
            catch (Exception e)
            {
                logger.LogError(e, MessageTemplate, e.GetType().Name);

                await WriteResponseAsJsonAsync(
                    context,
                    HttpStatusCode.InternalServerError,
                    new ErrorResponse
                    {
                        Title = e.Message,
                        StackTrace = e.StackTrace
                    });
            }
        }

        private Task WriteResponseAsJsonAsync(HttpContext context, HttpStatusCode statusCode, ErrorResponse payload)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            dynamic output = new ExpandoObject();
            output.title = payload.Title;
            output.traceIdentifier = context.TraceIdentifier;

            if (hostEnvironment.IsDevelopment())
            {
                output.stackTrace = payload.StackTrace;
            }

            if (payload.Errors != null)
            {
                output.errors = payload.Errors;
            }

            string json = JsonSerializer.Serialize(output, jsonSerializerOptions);

            return context.Response.WriteAsync(json);
        }
    }
}
