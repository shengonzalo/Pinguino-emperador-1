using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using OGA.AAS.BackEnd.Application.Registration;
using OGA.AAS.BackEnd.Business.Registration;
using OGA.AAS.BackEnd.Infrastructure.Registration;
using OGA.AAS.BackEnd.WebAPI.Builders;
using OGA.AAS.BackEnd.WebAPI.Library.Middleware.CurrentUser;
using OGA.AAS.BackEnd.WebAPI.Registration;
using OGA.Core.BackEnd.Domain.Enums;
using OGA.Core.BackEnd.WebAPI.Handlers.Authentication;
using OGA.Core.BackEnd.WebAPI.Middleware.HealthcheckRedirect;
using OGA.Core.BackEnd.WebAPI.Middleware.HttpResponseException;
using OGA.Core.BackEnd.WebAPI.Middleware.Logging;
using OGA.Core.BackEnd.WebAPI.Middleware.SecurityHeader;
using System.Text;
using ConfigurationManager = OGA.AAS.BackEnd.Application.Registration.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials();
    });
});

builder.Services.AddHsts(options =>
{
    options.MaxAge = TimeSpan.FromDays(365);
    options.IncludeSubDomains = true;
    options.Preload = true;
});

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddAASBusinessServices();
builder.Services.AddAASApplicationServices(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null)
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", null)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuers = [ConfigurationManager.JwtIssuer],
            ValidAudiences = [ConfigurationManager.JwtAudience],
            IssuerSigningKeys =
            [
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.JwtKey))
            ],
            AuthenticationType = ConfigurationManager.JwtAuthType
        };
    });

var database = string.IsNullOrEmpty(ConfigurationManager.DatabaseType) ? (int)DatabaseEnum.SqlServer :
                   int.Parse(ConfigurationManager.DatabaseType);

if (database == (int)DatabaseEnum.PostgreSql)
{
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
}

builder.Services.AddAASInfrastructureServices(builder.Configuration);

builder.Services.AddSwaggerServices();

builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<HttpResponseExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<SecurityHeaderMiddleware>();
app.UseMiddleware<CurrentUserMiddleware>();
app.UseMiddleware<HealthcheckRedirectMiddleware>();

app.AddSwaggerApp();

app.MapControllers();
app.MapDefaultControllerRoute();

app.Use(async (context, next) => {
    context.Request.EnableBuffering();
    context.Features.Get<IHttpMaxRequestBodySizeFeature>()!.MaxRequestBodySize = long.MaxValue;
    await next.Invoke();
});

app.Run();
