using Domain.Common;
using Infrastructure.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
    }

    private static void AddMySql(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MySql")!;

        services.AddDbContext<DatabaseContext>(x =>
            x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options =>
            {
                options.MigrationsAssembly("Infrastructure");
                options.EnableRetryOnFailure();
            }));
        
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        
        services.AddHealthChecks()
            .AddDbContextCheck<DatabaseContext>()
            .AddMySql(connectionString);
    }
}