using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OGA.Core.BackEnd.Application.Registration;
using System.Reflection;

namespace OGA.AAS.BackEnd.Application.Registration
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddAASApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigurationManager.Configuration = configuration;

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddCoreApplicationServices(configuration);

            services.AddMenuServices();
            services.AddUserServices();
            services.AddRoleServices();
            services.AddLanguageService();
            services.AddIdentifierTypeService();
            services.AddUser2FACodeServices();

            return services;
        }
    }
}
