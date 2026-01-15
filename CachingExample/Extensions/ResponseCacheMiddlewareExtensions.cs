using CachingExample.Middleware;

namespace CachingExample.Extensions
{
    public static class ResponseCacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseCachingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseCacheMiddleware>();
        }
    }
}
