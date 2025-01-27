using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace poc.aws.services.api.Configuration;

public static class ConfigureHealthCheck
{
    public static void AddHealthCheckConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

        services.AddHealthChecks()
            .AddNpgSql(
                connectionString: connectionString,
                healthQuery: "SELECT 1;",
                name: "postgres",
                failureStatus: HealthStatus.Degraded,
                tags: ["db", "sql", "postgres"]);
    }

    public static void AddHealthCheckMap(this WebApplication app) =>
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var response = new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(entry => new
                    {
                        name = entry.Key,
                        status = entry.Value.Status.ToString(),
                        description = entry.Value.Description
                    })
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        });
}