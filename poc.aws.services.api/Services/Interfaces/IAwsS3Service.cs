using Amazon.S3.Model;

namespace poc.aws.services.api.Services.Interfaces;

public interface IAwsS3Service
{
    Task<bool> UploadFileToS3Async(IFormFile file, string newFileName, CancellationToken ct);
    Task<GetObjectResponse> DownloadFileAsync(string fileName, CancellationToken ct);
    Task<DeleteObjectResponse> DeleteFileAsync(string fileName, CancellationToken ct);
    Task<string> UploadFilePresignedAsync(string fileName, string contentType);
    Task<string> GetPresignedUrlAsync(string fileName);
    Task CreateBucketAsync(CancellationToken ct);
}