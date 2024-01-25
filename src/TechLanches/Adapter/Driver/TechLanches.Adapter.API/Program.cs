using Microsoft.AspNetCore.Builder;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Headers;
using TechLanches.Adapter.API.Configuration;
using TechLanches.Adapter.API.Middlewares;
using TechLanches.Adapter.RabbitMq.Options;
using TechLanches.Adapter.SqlServer;
using TechLanches.Application;
using TechLanches.Application.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
    .AddEnvironmentVariables();

AppSettings.Configuration = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Setting Swagger
builder.Services.AddSwaggerConfiguration();

//DI Abstraction
builder.Services.AddDependencyInjectionConfiguration();

//Setting DBContext
builder.Services.AddDatabaseConfiguration(builder.Configuration);

//Setting mapster
builder.Services.RegisterMaps();

builder.Services.Configure<RabbitOptions>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection("ApiMercadoPago"));

//Setting healthcheck
builder.Services.AddHealthCheckConfig(builder.Configuration);

//Criar uma politica de retry (tente 3x, com timeout de 3 segundos)
var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                  .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

//Registrar httpclient
builder.Services.AddHttpClient("MercadoPago", httpClient =>
{
    httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", builder.Configuration.GetSection($"ApiMercadoPago:AccessToken").Value);
    httpClient.BaseAddress = new Uri(builder.Configuration.GetSection($"ApiMercadoPago:BaseUrl").Value);
}).AddPolicyHandler(retryPolicy);

var app = builder.Build();

app.AddCustomMiddlewares();

app.UseDatabaseConfiguration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseRouting();

app.UseSwaggerConfiguration();

app.AddHealthCheckEndpoint();

app.UseMapEndpointsConfiguration();

app.UseStaticFiles();

app.Run();