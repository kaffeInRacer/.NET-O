using Serilog;
using User.Infrastructure.Extension;
using Infrastructure.Logger.Extension;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using User.Application.Interfaces.IUseCase;
using User.Application.Interfaces.IRepository;
using User.Application.UseCase;
using User.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CorsPolicy",
        policy  =>
        {
            policy.WithOrigins("*");
            policy.WithMethods("GET", "POST", "PUT", "DELETE");
            policy.WithHeaders("Access-Control-Allow-Origin");
        });
});
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddMinIO(builder.Configuration);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserUseCase, UserUseCase>();

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

// app.UseHttpsRedirection();
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
