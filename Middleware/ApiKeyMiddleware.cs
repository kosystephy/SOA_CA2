using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Data;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SOA_CA2_E_Commerce.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            
            if (context.Request.Path.StartsWithSegments("/api/Auth/register", StringComparison.OrdinalIgnoreCase) ||
                context.Request.Path.StartsWithSegments("/api/Auth/login", StringComparison.OrdinalIgnoreCase) ||
                (context.Request.Path.StartsWithSegments("/api/Product", StringComparison.OrdinalIgnoreCase) && context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase)) ||
                (context.Request.Path.StartsWithSegments("/api/Category", StringComparison.OrdinalIgnoreCase) && context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase)))
            {
                await _next(context);
                return;
            }

           
            if (!context.Request.Headers.TryGetValue("ApiKey", out var apiKeyValues))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key is missing.");
                return;
            }

          
            string apiKey = apiKeyValues.ToString();

     
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ApiKey == apiKey && u.ApiKeyExpiration > DateTime.UtcNow);

            if (user == null)
            {
                await context.Response.WriteAsync("Invalid or expired API Key.");
                return;
            }

            await _next(context);
        }
    }
}
