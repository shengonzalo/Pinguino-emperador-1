using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace OGA.Base.BackEnd.WebAPI.Registration
{
    [ExcludeFromCodeCoverage]
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
                        Description = @"
En el apartado que encontrarás a continuación, se describe en detalle la forma de consumir cada uno de los endpoints que conforman la API de **oga.Logistics**. Todas las llamadas deben contener las siguientes cabeceras:

- **Authorization**
    - Basic *{base64(user:password)}*. Solo para `POST /token`
    - Bearer *{jwt_token}*

- **x-timezone** *{timezone}*. Por ejemplo: `Europe/Madrid`
"
                    });
                    options.EnableAnnotations();

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = JwtBearerDefaults.AuthenticationScheme
                    });

                    options.AddSecurityDefinition("Basic", new OpenApiSecurityScheme()
                    {
                        Description = "Basic authentication using a username and password. Example: \"Authorization: Basic {base64(user:password)}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "Basic"
                    });

                    options.TagActionsBy(api =>
                    {
                        return [api.RelativePath!.Split('/')[0]];
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = JwtBearerDefaults.AuthenticationScheme
                                }
                            },
                            Array.Empty<string>()
                        },
                        {
                             new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Id = "Basic",
                                     Type = ReferenceType.SecurityScheme
                                 }
                             },
                             Array.Empty<string>()
                         }
                    });

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
