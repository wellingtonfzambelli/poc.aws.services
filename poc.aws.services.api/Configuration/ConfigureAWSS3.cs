using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Options;
using poc.aws.services.api.Configuration.Settings;

namespace poc.aws.services.api.Configuration;

public static class ConfigureAWSS3
{
    public static void AddS3Configuration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        CreateS3Settings(builder);

        builder.Services.AddSingleton<IAmazonS3>(sp =>
        {
            var s3Settings = sp.GetRequiredService<IOptions<S3Settings>>().Value;
            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(s3Settings.Region)
            };

            if (s3Settings.UseLocalStack)
            {
                config.ServiceURL = "https://localhost.localstack.cloud:4566";
                config.ForcePathStyle = true;
                config.UseHttp = true;
            }

            return new AmazonS3Client(config);
        });
    }

    private static void CreateS3Settings(WebApplicationBuilder builder)
    {
        builder.Services.Configure<S3Settings>(builder.Configuration.GetSection("S3Settings"));
        builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<S3Settings>>().Value);
    }
}