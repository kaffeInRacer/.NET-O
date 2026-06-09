using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using User.Application.Interfaces.IS3Storage;

namespace User.Infrastructure.S3storage;

public class S3StorageService : IS3StorageService
{
    private readonly IMinioClient _minioClient;
    private readonly S3StorageOptions _options;

    public S3StorageService(IOptions<S3StorageOptions> options)
    {
        _options = options.Value;

        _minioClient = new MinioClient()
            .WithEndpoint(_options.ServiceUrl, _options.Port)
            .WithCredentials(_options.AccessKey, _options.SecretKey)
            .WithSSL(_options.UseSSL)
            .Build();
    }

    public async Task InitializeAsync()
    {
        var exists = await _minioClient.BucketExistsAsync(
            new BucketExistsArgs().WithBucket(_options.BucketName));

        if (exists) return;

        await _minioClient.MakeBucketAsync(
            new MakeBucketArgs().WithBucket(_options.BucketName));

        var policy = $$"""
        {
            "Version": "2012-10-17",
            "Statement": [
                {
                    "Effect": "Allow",
                    "Principal": "*",
                    "Action": "s3:GetObject",
                    "Resource": "arn:aws:s3:::{{_options.BucketName}}/*"
                }
            ]
        }
        """;

        await _minioClient.SetPolicyAsync(new SetPolicyArgs()
            .WithBucket(_options.BucketName)
            .WithPolicy(policy));
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        var ext = Path.GetExtension(fileName);
        var key = $"{Guid.NewGuid()}{ext}";

        await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(_options.BucketName)
            .WithObject(key)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType));

        return $"{_options.BucketName}/{key}";
    }

    public async Task DeleteAsync(string fileKey)
    {
        await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(_options.BucketName)
            .WithObject(fileKey));
    }

    public async Task<string> GetUrlAsync(string fileKey)
    {
        var url = await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
            .WithBucket(_options.BucketName)
            .WithObject(fileKey)
            .WithExpiry(60 * 60 * 24));

        return url;
    }
}
