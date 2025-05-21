using System.Reflection;
using System.Text.RegularExpressions;

namespace Acesur.Aps.BackEnd.Application.Extensions
{
    public static class ExtensionMethods
    {
        public static async Task<object?> InvokeAsync(this MethodInfo @this, object? obj, params object?[]? parameters)
        {
            dynamic awaitable = @this.Invoke(obj, parameters)!;
            await awaitable;
            return awaitable!.GetAwaiter().GetResult();
        }
        public static string RemoveAllWhiteSpace(this string @this)
        {
            Regex sWhitespace = new(@"\s+");
            return sWhitespace.Replace(@this, "");
        }
    }
}
