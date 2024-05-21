using System.Collections.Immutable;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Presentation.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static void AddJwt(this IServiceCollection services, IConfiguration configuration) => services
        .AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = $"https://{configuration["Jwt:Domain"]}";
            options.Audience = configuration["Jwt:Audience"];
        });
    
    internal static void AddSwagger(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddSwaggerGen(x =>
        {
            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Description = "JWT authorization header using the bearer scheme.",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = "Bearer",
                Type = SecuritySchemeType.OAuth2
            });
            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    ImmutableArray<string>.Empty
                }
            });
            x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml"));
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Contact = new OpenApiContact
                {
                    Email = "example@template.com"
                },
                Description = "API to manage the MyMinimalCleanArchWithAuth0 application.",
                License = new OpenApiLicense
                {
                    Name = "© All rights reserved."
                },
                Title = "MyMinimalCleanArchWithAuth0",
                Version = "v1"
            });
        });
    }
}