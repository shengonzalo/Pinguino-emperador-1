using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.Core.BackEnd.Domain.Models;
using System.Security.Claims;

namespace OGA.AAS.BackEnd.WebAPI.Library.Middleware.Token
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var existsAuthorization = context.Request.Headers.TryGetValue("Authorization", out var token);
            var isEndpointRefresh = context.Request.Path.Equals("/token/refresh", StringComparison.OrdinalIgnoreCase);
            if (existsAuthorization && !isEndpointRefresh && !token.ToString().ToLower().Contains("basic"))
            {
                string userId = context.User.FindFirstValue("uid");
                string languageCode = context.User.FindFirstValue("langcode");
                var email = context.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value.ToString();
                IEnumerable<string> roleId = from yu in context.User.FindAll("http://schemas.microsoft.com/ws/2008/06/identity/claims/role") select yu.Value.ToString();

                var tokenInfoModel = new TokenInfoModel
                {
                    UserId = userId,
                    Email = email,
                    LanguageCode = languageCode,
                    RoleId = roleId
                };

                ITokenService tokenService = context.RequestServices.GetRequiredService<ITokenService>();
                var isValid = await tokenService.IsValidToken(tokenInfoModel);
                if (!isValid)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token invalid");

                    return;
                }
            }

            await _next(context);
        }
    }
}
