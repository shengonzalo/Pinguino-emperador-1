using Microsoft.Extensions.DependencyInjection;
using OGA.AAS.BackEnd.Business.Registration;
using OGA.Base.BackEnd.Business.Services;
using OGA.Base.BackEnd.Domain.Contracts.Services;
using OGA.Core.BackEnd.Business.Registration;
using System.Diagnostics.CodeAnalysis;

namespace OGA.Base.BackEnd.Business.Registration;

[ExcludeFromCodeCoverage]
public static class BusinessRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ISsoAuthService, SsoAuthService>();

        services.AddCoreBusinessServices();
        services.AddAASBusinessServices();

        services.AddScoped<IAuditService, AuditService>();

        return services;
    }
}
