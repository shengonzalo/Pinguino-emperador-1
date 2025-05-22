using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace OGA.AAS.BackEnd.WebAPI.Registration
{
    public static class SwaggerRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            var enabled = Convert.ToBoolean(Application.Registration.ConfigurationManager.SwaggerEnabled);

            if (enabled)
            {
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = Application.Registration.ConfigurationManager.WebAPIName,
                        Description = "API que maneja el dominio de OGA Authorization and Authentication Service"
                    });
                    options.EnableAnnotations();

                    options.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Basic scheme. Example: \"Authorization: Basic {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Basic"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Basic"
                                }
                            },
                            new string[]{}
                        }
                    });

                    // using System.Reflection;
                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                    options.ExampleFilters();
                });

                services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
                services.AddFluentValidationRulesToSwagger();
            }

            return services;
        }
    }
}
