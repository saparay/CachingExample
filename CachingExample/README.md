# ASP.NET Core Web API -- In-Memory Cache Middleware

This project demonstrates how to implement **custom In-Memory caching
using middleware** in **ASP.NET Core Web API**. The middleware caches
HTTP **GET responses** to improve performance and reduce redundant
processing.

------------------------------------------------------------------------

## ?? Features

-   Custom Response Caching Middleware
-   Uses IMemoryCache
-   Caches GET requests only
-   Cache key based on URL + Query Parameters
-   Configurable Time To Live (TTL)
-   Centralized and reusable caching logic
-   Production-ready structure

------------------------------------------------------------------------

## ?? When to Use This

? Read-heavy APIs\
? Public or reference data\
? APIs with low change frequency

? POST / PUT / DELETE requests\
? User-specific or sensitive data\
? Frequently changing data

------------------------------------------------------------------------

## ??? Project Structure

    ??? Controllers
    ?   ??? WeatherController.cs
    ??? Middleware
    ?   ??? ResponseCacheMiddleware.cs
    ??? Extensions
    ?   ??? ResponseCacheMiddlewareExtensions.cs
    ??? Models
    ?   ??? CacheOptions.cs
    ??? Program.cs
    ??? README.md

------------------------------------------------------------------------

## ?? Prerequisites

-   .NET 7 or later
-   ASP.NET Core Web API
-   Microsoft.Extensions.Caching.Memory

Install the required package (if not already present):

    dotnet add package Microsoft.Extensions.Caching.Memory

------------------------------------------------------------------------

## ?? Setup & Configuration

### Register In-Memory Cache

    builder.Services.AddMemoryCache();

### Register Cache Middleware

    app.UseResponseCachingMiddleware();

?? Important: The middleware must be registered **before**
MapControllers().

------------------------------------------------------------------------

## ?? How the Middleware Works

1.  Intercepts incoming requests
2.  Allows only GET requests to be cached
3.  Generates a cache key using:
    -   Request path
    -   Query parameters
4.  Checks cache:
    -   If present ? returns cached response
    -   If absent ? executes request and stores response
5.  Returns response to the client

------------------------------------------------------------------------

## ?? Sample API Endpoint

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Time = DateTime.UtcNow,
            Message = "Cached Response"
        });
    }

------------------------------------------------------------------------

## ?? Cache Duration

Default cache expiration:

    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);

This value can be adjusted as needed.

------------------------------------------------------------------------

## ?? Limitations

-   Cache is stored in application memory
-   Cache is cleared on application restart
-   Not suitable for multi-instance deployments

? Use Redis or IDistributedCache for distributed environments

------------------------------------------------------------------------

## ?? Possible Enhancements

-   Attribute-based caching
-   Cache invalidation strategies
-   Redis / Distributed caching
-   Status code-based caching (cache only 200 OK)
-   Logging cache hits and misses

------------------------------------------------------------------------

## ?? Interview Tip

Middleware-based caching provides centralized, reusable caching logic
that keeps controllers clean and improves application performance.

------------------------------------------------------------------------

## ?? License

This project is intended for learning and demonstration purposes.

------------------------------------------------------------------------

## ?? Author

Mani Chandra Saparay\
Full Stack Developer -- ASP.NET Core \| React \| Azure
