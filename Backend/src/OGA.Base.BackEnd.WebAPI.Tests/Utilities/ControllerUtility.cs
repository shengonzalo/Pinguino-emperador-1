using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace OGA.Base.BackEnd.WebAPI.Tests.Utilities
{
    public static class ControllerUtility
    {
        public static ControllerContext GetControllerContext()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]{
                new Claim("uid", "1"),
                new Claim(ClaimTypes.Email, "admin@admin.com")
            }, "claims"));

            var httpContext = new DefaultHttpContext() { User = user };
            httpContext.Request.Headers["Authorization"] = "Basic YWRtaW5AY29tcGFueS5jb206Z2hLUSFsNWlicg==";

            KeyValuePair<string, object?>[] values = new KeyValuePair<string, object?>[2];
            values[0] = new KeyValuePair<string, object?>("controller", "controller");
            values[1] = new KeyValuePair<string, object?>("action", "action");
            var routeData = new RouteData(RouteValueDictionary.FromArray(values));

            return new ControllerContext()
            {
                HttpContext = httpContext,
                RouteData = routeData
            };
        }
    }
}
