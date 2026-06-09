namespace User.Application.Interfaces.IS3Storage;

public interface IS3StorageService
{
    Task InitializeAsync();
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);
    Task DeleteAsync(string fileKey);
    Task<string> GetUrlAsync(string fileKey);
}