    using System.Net;
    using System.Text.Json;
    using Microsoft.AspNetCore.Diagnostics;
    using TodoApp.Core.Exceptions;

    namespace TodoApp.API.Middleware;

    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var correlationId = Guid.NewGuid().ToString();
            context.Response.Headers["X-Correlation-Id"] = correlationId;

            // For GraphQL requests, return GraphQL-compatible error format
            if (context.Request.Path.StartsWithSegments("/graphql"))
            {
                var errorResponse = new
                {
                    errors = new[]
                    {
                        new
                        {
                            message = _env.IsDevelopment() ? exception.Message : "An internal server error occurred",
                            extensions = new
                            {
                                code = "INTERNAL_SERVER_ERROR",
                                correlationId
                            }
                        }
                    }
                };

                return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }

            // For non-GraphQL requests, return ProblemDetails format
            var problemDetails = new
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "Internal Server Error",
                Status = context.Response.StatusCode,
                Detail = _env.IsDevelopment() ? exception.ToString() : "An internal server error occurred",
                Instance = context.Request.Path,
                CorrelationId = correlationId
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }

    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }