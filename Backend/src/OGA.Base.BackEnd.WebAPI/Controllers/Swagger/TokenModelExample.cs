using OGA.AAS.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.WebAPI.Controllers.Swagger
{
    [ExcludeFromCodeCoverage]
    public class TokenModelExample : IExamplesProvider<TokenModel>
    {
        public TokenModel GetExamples()
        {
            return new TokenModel
            {
                AccessToken = "eyfagfegrwgrggre.eydwefwegwegggdsa",
                Email = "admin@admin.com",
                ExpiredDate = "10/01/2022 14:00:00.000"
            };
        }
    }
}
