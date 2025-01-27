namespace poc.aws.services.api.Configuration.Settings;

public sealed class S3Settings
{
    public string Region { get; init; } = string.Empty;
    public string BucketName { get; init; } = string.Empty;
    public int PresignedInMinutes { get; init; } = 0;
    public bool UseLocalStack { get; init; } = false;
}