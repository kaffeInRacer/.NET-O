using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Interfaces.IS3Storage;
using User.Infrastructure.S3storage;

namespace User.Infrastructure.Extension;

public static class MinIOExtension
{
    public static IServiceCollection AddMinIO(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("MinIO");
        services.Configure<S3StorageOptions>(section);
        services.AddScoped<IS3StorageService, S3StorageService>();
        return services;
    }
}