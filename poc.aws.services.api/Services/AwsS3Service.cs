using Amazon.S3;
using Amazon.S3.Model;
using poc.aws.services.api.Configuration.Settings;
using poc.aws.services.api.Services.Interfaces;

namespace poc.aws.services.api.Services;

public sealed class AwsS3Service : IAwsS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly S3Settings _s3Settings;

    public AwsS3Service(IAmazonS3 s3Client, S3Settings s3Settings)
    {
        _s3Client = s3Client;
        _s3Settings = s3Settings;
    }

    public async Task<bool> UploadFileToS3Async(IFormFile file, string newFileName, CancellationToken ct)
    {
        using var stream = file.OpenReadStream();

        var putRequest = new PutObjectRequest
        {
            BucketName = _s3Settings.BucketName,
            Key = $"images/{newFileName}",
            InputStream = stream,
            ContentType = file.ContentType,
            Metadata =
            {
                ["file-name"] = file.FileName
            }
        };

        await _s3Client.PutObjectAsync(putRequest, ct);

        return true;
    }

    public async Task<GetObjectResponse> DownloadFileAsync(string fileName, CancellationToken ct)
    {
        var getRequest = new GetObjectRequest
        {
            BucketName = _s3Settings.BucketName,
            Key = $"images/{fileName}"
        };

        return await _s3Client.GetObjectAsync(getRequest, ct);
    }

    public async Task<DeleteObjectResponse> DeleteFileAsync(string fileName, CancellationToken ct)
    {
        var getRequest = new DeleteObjectRequest
        {
            BucketName = _s3Settings.BucketName,
            Key = $"images/{fileName}"
        };

        return await _s3Client.DeleteObjectAsync(getRequest, ct);
    }

    public async Task<string> UploadFilePresignedAsync(string fileName, string contentType)
    {
        var key = Guid.NewGuid();
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _s3Settings.BucketName,
            Key = $"images/{key}",
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.AddMinutes(_s3Settings.PresignedInMinutes),
            ContentType = contentType,
            Metadata =
            {
                ["file-name"] = fileName
            }
        };

        return await _s3Client.GetPreSignedURLAsync(request);
    }

    public async Task<string> GetPresignedUrlAsync(string fileName)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _s3Settings.BucketName,
            Key = $"images/{fileName}",
            Verb = HttpVerb.GET,
            Expires = DateTime.UtcNow.AddMinutes(_s3Settings.PresignedInMinutes)
        };

        return await _s3Client.GetPreSignedURLAsync(request);
    }

    public async Task CreateBucketAsync(CancellationToken ct)
    {
        var bucketRequest = new PutBucketRequest
        {
            BucketName = _s3Settings.BucketName
        };

        await _s3Client.PutBucketAsync(bucketRequest, ct);
    }
}