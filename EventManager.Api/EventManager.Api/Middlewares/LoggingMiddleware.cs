using System.Diagnostics;

namespace EventManager.Api.Middlewares
{
    /// <summary>
    /// Http request and response logging middleware
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("HTTP {Method} {Path} started",
                context.Request.Method,
                context.Request.Path);

            var watch = Stopwatch.StartNew();
            await _next(context);
            watch.Stop();

            _logger.LogInformation("HTTP {Method} {Path} finished in {ElapsedMilliseconds}ms with status {StatusCode}",
                context.Request.Method,
                context.Request.Path,
                watch.ElapsedMilliseconds,
                context.Response.StatusCode);
        }
    }
}
