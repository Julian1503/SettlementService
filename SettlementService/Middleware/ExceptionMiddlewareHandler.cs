using System.Net;
using System.Text.Json;
using SettlementService.Constants.General;
using SettlementService.Domain.Abstractions;

namespace SettlementService.API.Middleware
{
    /// <summary>
    /// Middleware that handles exceptions thrown during the request pipeline.
    /// </summary>
    public class ExceptionMiddlewareHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddlewareHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddlewareHandler"/> class.
        /// </summary>
        /// <param name="next">The next delegate in the request pipeline.</param>
        /// <param name="logger">Logger used to log exception details.</param>
        public ExceptionMiddlewareHandler(RequestDelegate next, ILogger<ExceptionMiddlewareHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware. If an exception occurs, it logs the exception and handles it.
        /// </summary>
        /// <param name="httpContext">The HTTP context for the request.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handles exceptions by setting the response status code to 500 and writing a JSON error response.
        /// </summary>
        /// <param name="context">The HTTP context for the request.</param>
        /// <param name="exception">The exception that was thrown.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            Error error = ErrorConstants.InternalServerError;
            var errorJson = JsonSerializer.Serialize(error);
            return context.Response.WriteAsync(errorJson);
        }
    }
}
