using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.WebAPI.Controllers.Swagger
{
    [ExcludeFromCodeCoverage]
    public class HealthCheckModelExample : IExamplesProvider<string>
    {
        public string GetExamples()
        {
            return DateTime.UtcNow.ToString(Application.Registration.ConfigurationManager.LongDateFormat);
        }
    }
}
