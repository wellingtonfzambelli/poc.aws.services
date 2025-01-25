using Npgsql;
using System.Data;

namespace poc.aws.services.api.Configuration;

public static class DapperConfig
{
    public static void AddDapperConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        services.AddTransient<IDbConnection>(sp =>
            new NpgsqlConnection(connectionString));
    }
}