using Serilog;
using User.Application.Options;
using User.Infrastructure.Extension;
using Infrastructure.Logger.Extension;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();

builder.Services.Configure<DeviceLimitOptions>(builder.Configuration.GetSection("DeviceLimit"));
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddMinIO(builder.Configuration);
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CorsPolicy",
        policy  =>
        {
            policy.WithOrigins("*");
            policy.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
            policy.WithHeaders("Access-Control-Allow-Origin");
        });
});


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.MapControllers();
app.Lifetime.ApplicationStarted.Register(() =>
{
    var server = app.Services.GetRequiredService<IServer>();
    var addresses = server.Features.Get<IServerAddressesFeature>();

    if (addresses is not null)
    {
        foreach (var address in addresses.Addresses)
        {
            Log.Information("Listening on {Address}", address);
        }
    }
});
app.Run();
