using Infrastructure.Logger.Middleware;
using Infrastructure.Logger.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Infrastructure.Logger.Extension;


public static class LoggerExtension
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        var options = builder.Configuration
            .GetSection("Serilog")
            .Get<SerilogOptions>() ?? new SerilogOptions();

        var minimumLevel = options.MinimumLevel switch
        {
            "Debug"   => LogEventLevel.Debug,
            "Warning" => LogEventLevel.Warning,
            "Error"   => LogEventLevel.Error,
            "Fatal"   => LogEventLevel.Fatal,
            _         => LogEventLevel.Information
        };

        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Is(minimumLevel)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName()
            .Enrich.WithThreadId()
            .WriteTo.Console(outputTemplate: options.TemplateFormat);

        if (options.WriteToFile)
        {
            var rollingInterval = options.RollingInterval switch
            {
                "Hour"  => RollingInterval.Hour,
                "Month" => RollingInterval.Month,
                _       => RollingInterval.Day
            };

            loggerConfig.WriteTo.File(
                options.FilePath,
                rollingInterval: rollingInterval,
                outputTemplate: options.TemplateFormat);
        }

        Log.Logger = loggerConfig.CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }

    public static IApplicationBuilder UseSerilogRequestLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CorrelationIdMiddleware>();
    }
}