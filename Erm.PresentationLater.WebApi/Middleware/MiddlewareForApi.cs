using Erm.PresentationLater.WebApi.Authorization;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Erm.PresentationLater.WebApi.Middleware
{
    public class MiddlewareForApi
    {
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;

        public MiddlewareForApi(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key is not Provided");
                return;
            }

           var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName) ?? "Null";

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Api key");
                return;
            }

            await _next(context);
        }
    }
}
