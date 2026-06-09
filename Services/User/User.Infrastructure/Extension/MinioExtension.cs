using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Interfaces.IS3Storage;
using User.Infrastructure.S3storage;

namespace User.Infrastructure.Extension;

public static class MinIOExtension
{
    public static IServiceCollection AddMinIO(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<S3StorageOptions>(options => configuration.GetSection("MinIO").Bind(options));
        services.AddSingleton<IS3StorageService, S3StorageService>();
        services.AddHostedService<MinioInitializerService>();
        return services;
    }
}
