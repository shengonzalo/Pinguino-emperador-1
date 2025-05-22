using Swashbuckle.AspNetCore.Filters;

namespace OGA.AAS.BackEnd.WebAPI.Controllers.Swagger
{
    public class HealthCheckModelExample : IExamplesProvider<string>
    {
        public string GetExamples()
        {
            return DateTime.UtcNow.ToString(Application.Registration.ConfigurationManager.LongDateFormat);
        }
    }
}
