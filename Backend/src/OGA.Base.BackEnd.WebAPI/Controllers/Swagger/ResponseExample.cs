using OGA.Core.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.WebAPI.Controllers.Swagger
{
    [ExcludeFromCodeCoverage]
    public class OkResponseModelExample : IExamplesProvider<OkResponseModel>
    {
        public OkResponseModel GetExamples()
        {
            return new OkResponseModel
            {
                Id = 1,
                Message = "Mensaje"
            };
        }
    }
}
