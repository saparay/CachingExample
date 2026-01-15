using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace CachingExample.Middleware
{
    public class ResponseCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public ResponseCacheMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Cache only GET requests
            if (context.Request.Method != HttpMethods.Get)
            {
                await _next(context);
                return;
            }

            var cacheKey = GenerateCacheKey(context.Request);

            if (_cache.TryGetValue(cacheKey, out string cachedResponse))
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(cachedResponse);
                return;
            }

            // Capture the response
            var originalBodyStream = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context);

            // Read response
            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

            // Cache response
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
            };

            _cache.Set(cacheKey, responseBody, cacheOptions);

            // Return response
            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }

        private static string GenerateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);

            foreach (var (k, v) in request.Query.OrderBy(x => x.Key))
            {
                key.Append($"|{k}:{v}");
            }

            return key.ToString();
        }
    } 
}
