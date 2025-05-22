using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OGA.AAS.BackEnd.Domain.Contracts.Persistence;
using OGA.AAS.BackEnd.Infrastructure.Context;
using OGA.AAS.BackEnd.Infrastructure.Persistence;
using OGA.Core.BackEnd.Infrastructure.Azure.Registration;
using OGA.Core.BackEnd.Infrastructure.Hash.Registration;
using OGA.Core.BackEnd.Infrastructure.Jwt.Registration;
using OGA.Core.BackEnd.Infrastructure.Logging.Registration;
using OGA.Core.BackEnd.Infrastructure.Registration;

namespace OGA.AAS.BackEnd.Infrastructure.Registration
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddAASInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options => 
            { 
                Core.BackEnd.Infrastructure.Registration.InfrastructureRegistration.RegisterDatabase(options); 
            });

            services.AddCoreInfrastructureServices();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserKeyRepository, UserKeyRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>(); 
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IIdentifierTypeRepository, IdentifierTypeRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IRolePermissionGroupRepository, RolePermissionGroupRepository>();
            services.AddScoped<IUser2FACodeRepository, User2FACodeRepository>();

            services.AddLoggingServices(configuration);
            services.AddJwtServices();
            services.AddHashServices();
            services.AddAzureServices();

            return services;
        }
    }
}
