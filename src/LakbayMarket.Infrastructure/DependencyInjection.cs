using LakbayMarket.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microting.EntityFrameworkCore.MySql.Infrastructure;

namespace LakbayMarket.Infrastructure;

public static class DependencyInjection
{
    // MySQL 8.4 LTS is the version we develop and test against. A fixed version is used instead of
    // ServerVersion.AutoDetect, which would open an extra connection to detect it.
    private static readonly Version MySqlVersion = new(8, 4, 0);

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<LakbayDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(MySqlVersion)));

        return services;
    }
}
