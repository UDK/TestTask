using Autofac;
using Autofac.Extensions.DependencyInjection;
using TestTask.DB.Repositories;
using MediatR.Extensions.Autofac.DependencyInjection;
using TestTask.Infrasctructure.Configuration;
using TestTask.Infrasctructure;
using TestTask.Application;

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
    q.RegisterModule(new TaskStartupAutoFac(builder.Configuration.GetSection("ConfigureSettings").Get<ConfigureSettings>()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
