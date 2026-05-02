
    using System.Net;
    using System.Text.Json;
    using Shareds.Exceptions;

    namespace final_project.API.Middlewares
    {
        public class ExceptionMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger<ExceptionMiddleware> _logger;

            public ExceptionMiddleware(RequestDelegate next,
                                       ILogger<ExceptionMiddleware> logger)
            {
                _next = next;
                _logger = logger;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);

                    context.Response.ContentType = "application/json";

                    int statusCode = ex switch
                    {
                        BaseException be => be.StatusCode,
                        UnauthorizedAccessException => 401,
                        _ => 500
                    };

                    context.Response.StatusCode = statusCode;

                    var response = new
                    {
                        statusCode,
                        message = ex.Message
                    };

                    var json = JsonSerializer.Serialize(response);

                    await context.Response.WriteAsync(json);
                }
            }
        }
    }