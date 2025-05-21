using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OGA.AAS.BackEnd.Infrastructure.Registration;
using OGA.Base.BackEnd.Domain.ApiClient;
using OGA.Base.BackEnd.Infrastructure.ApiClient.Repositories;
using OGA.Core.BackEnd.Infrastructure.Logging.Registration;
using OGA.Core.BackEnd.Infrastructure.Registration;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.Infrastructure.ApiClient.Registration;

[ExcludeFromCodeCoverage]
public static class InfrastructureApiClientRegistration
{
    public static IServiceCollection AddInfrastructureApiClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCoreInfrastructureServices();
        services.AddAASInfrastructureServices(configuration);

        services.AddScoped<IGraphRepository, GraphRepository>();

        services.AddLoggingServices(configuration);

        return services;
    }
}
