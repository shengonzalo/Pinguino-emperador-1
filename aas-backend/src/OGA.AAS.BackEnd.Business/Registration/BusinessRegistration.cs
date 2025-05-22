using Microsoft.Extensions.DependencyInjection;
using OGA.AAS.BackEnd.Business.Services;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.Domain.Custom;
using OGA.Core.BackEnd.Business.Registration;

namespace OGA.AAS.BackEnd.Business.Registration
{
    public static class BusinessRegistration
    {
        public static IServiceCollection AddAASBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IIdentifierTypeService, IdentifierTypeService>();
            services.AddScoped<IUser2FACodeService, User2FACodeService>();
            services.AddScoped<CurrentUserProvider>();

            services.AddCoreBusinessServices();

            return services;
        }
    }
}
