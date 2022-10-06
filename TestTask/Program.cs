using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection;
using Serilog;
using Serilog.Events;
using TaskApplication;
using TaskApplication.Application;
using TaskInfrastructure;
using TaskInfrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .Build();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(q =>
{
    q.RegisterMediatR(typeof(Program).Assembly);
    q.RegisterModule(new TaskModule(builder.Configuration.GetSection("ConfigureSettings").Get<ConfigureSettings>()));
});
builder.Host.UseSerilog();
builder.Logging.AddSerilog(new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs/logs.txt",
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(q =>
    {
        q.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
    });
}

app.MapControllers();

app.Run();
