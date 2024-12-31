
using System.Diagnostics;

namespace Restaurants.API.Middlewares
{
    public class TimeLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<TimeLoggingMiddleware> _logger;

        public TimeLoggingMiddleware(ILogger<TimeLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopWatch = Stopwatch.StartNew();
            await next.Invoke(context);
            stopWatch.Stop();

            _logger.LogInformation("Request [{verb}] at {path} took {time} ms",
                context.Request.Method,
                context.Request.Path,
                stopWatch.ElapsedMilliseconds);


        }
    }
}
