using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;

namespace Core.Application.Middlewares
{
    public class HttpContextLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpContextLoggingMiddleware> _logger;

        public HttpContextLoggingMiddleware(RequestDelegate next, ILogger<HttpContextLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Start the timer to measure request processing time
            var stopwatch = Stopwatch.StartNew();

            try
            {
                // Log Request details
                context.Request.EnableBuffering();  // Allows multiple reads of the request body
                var requestBody = await ReadRequestBodyAsync(context.Request);
                _logger.LogInformation("INCOMING Request: {method} {url} {body}",
                    context.Request.Method,
                    context.Request.Path,
                    requestBody);

                // Move to the next middleware
                await _next(context);

                // Log Response details
                var responseBody = await ReadResponseBodyAsync(context.Response);
                _logger.LogInformation("OUTGOING Response: {statusCode} {body}",
                    context.Response.StatusCode,
                    responseBody);
            }
            finally
            {
                stopwatch.Stop();
                // Log elapsed time
                _logger.LogInformation("Request processed in {ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);
            }
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.Body.Position = 0;
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;  // Reset stream position after reading
            return body;
        }

        private async Task<string> ReadResponseBodyAsync(HttpResponse response)
        {
            // Note: Accessing response body may require buffering, which can have memory implications.
            if (response.Body.CanSeek)
            {
                response.Body.Position = 0;
                using var reader = new StreamReader(response.Body, Encoding.UTF8, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                response.Body.Position = 0; // Reset stream position after reading
                return body;
            }
            return string.Empty;
        }
    }

}
