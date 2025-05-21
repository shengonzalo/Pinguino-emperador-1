using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OGA.AAS.BackEnd.Infrastructure.Registration;
using OGA.Base.BackEnd.Domain.Contracts.Persistence;
using OGA.Base.BackEnd.Infrastructure.Context;
using OGA.Base.BackEnd.Infrastructure.Persistence;
using OGA.Core.BackEnd.Infrastructure.Logging.Registration;
using OGA.Core.BackEnd.Infrastructure.Registration;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.Infrastructure.Registration
{
    [ExcludeFromCodeCoverage]
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(Core.BackEnd.Infrastructure.Registration.InfrastructureRegistration.RegisterDatabase);

            services.AddCoreInfrastructureServices();
            services.AddAASInfrastructureServices(configuration);

            services.AddLoggingServices(configuration);
            services.AddScoped<IActionsAuditRepository, ActionsAuditRepository>();
            return services;
        }
    }
}
