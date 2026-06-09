using Microsoft.Extensions.Hosting;
using User.Application.Interfaces.IS3Storage;

namespace User.Infrastructure.S3storage;

public class MinioInitializerService : IHostedService
{
    private readonly IS3StorageService _s3StorageService;

    public MinioInitializerService(IS3StorageService s3StorageService)
    {
        _s3StorageService = s3StorageService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _s3StorageService.InitializeAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
