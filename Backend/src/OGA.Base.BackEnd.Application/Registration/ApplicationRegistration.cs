using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OGA.AAS.BackEnd.Application.Registration;
using OGA.Core.BackEnd.Application.Registration;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace OGA.Base.BackEnd.Application.Registration
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigurationManager.Configuration = configuration;

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddCoreApplicationServices(configuration);
            services.AddAASApplicationServices(configuration);

            services.AddActionsAuditServices();

            return services;
        }
    }
}
