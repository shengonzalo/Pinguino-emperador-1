using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Custom;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Domain.Enums;
using System.Security.Claims;

namespace OGA.AAS.BackEnd.WebAPI.Library.Middleware.CurrentUser;

public class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, CurrentUserProvider currentUserProvider)
    {
        var existsAuthorization = context.Request.Headers.TryGetValue("Authorization", out var token);
        var existsApiKey = context.Request.Headers.TryGetValue("x-api-key", out var apiKey);

        var existsTimeZoneId = context.Request.Headers.TryGetValue("x-timezone", out var timeZoneId);
        timeZoneId = existsTimeZoneId ? timeZoneId : DateTimeEnum.Utc;

        if (existsAuthorization)
        {
            var isBasic = token.ToString().StartsWith("Basic ", StringComparison.OrdinalIgnoreCase);

            string userId = isBasic ? "0" : context.User.FindFirstValue("uid");
            var email = isBasic ? context.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value.ToString() :
                context.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value.ToString();
            IEnumerable<string> roleId = from yu in context.User.FindAll("http://schemas.microsoft.com/ws/2008/06/identity/claims/role") select yu.Value.ToString();

            var userToken = CheckToken(token);

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(email))
            {
                currentUserProvider.SetCurrentUser(userId, email!, roleId, userToken, timeZoneId);
            }
        }
        else if (existsApiKey)
        {
            IUserService userService = context.RequestServices.GetRequiredService<IUserService>();
            var user = await userService.GetUserByApiKey(apiKey);

            if (user != null)
            {
                var identity = (ClaimsIdentity)context.User.Identity!;
                identity?.AddClaim(new Claim("uid", user.Id.ToString()));
                
                var roleId = user.Roles != null ? user.Roles.Select(x => x.Id.ToString()) : new List<string>();
                currentUserProvider.SetCurrentUser(user.Id.ToString(), user.Email!, roleId, apiKey, timeZoneId);
            }
        }

        await _next(context);
    }

    private static string CheckToken(string token)
    {
        if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return token![7..];
        }
        else if (token.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            return token![6..];
        }            

        return string.Empty;
    }

}
