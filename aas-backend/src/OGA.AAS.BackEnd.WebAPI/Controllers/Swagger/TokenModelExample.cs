using OGA.AAS.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;

namespace OGA.AAS.BackEnd.WebAPI.Controllers.Swagger
{
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
