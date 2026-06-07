namespace User.Infrastructure.S3storage;

public class S3StorageOptions
{
    public string ServiceUrl { get; set; } = string.Empty;
    public int Port { get; set; } = 9000;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string BucketName { get; set; } = string.Empty;
    public bool UseSSL { get; set; } = false;
}