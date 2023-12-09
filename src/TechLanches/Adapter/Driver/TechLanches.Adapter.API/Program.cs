using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Headers;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago;
using TechLanches.Adapter.API.Configuration;
using TechLanches.Adapter.SqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
    .AddEnvironmentVariables();

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

//Criar uma politica de retry (tente 3x, com timeout de 3 segundos)
var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                  .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

//Registrar httpclient
builder.Services.AddHttpClient<IMercadoPagoService, MercadoPagoService>((serviceProvider, httpClient) =>
{
    //var mercadoPagoConfig = serviceProvider.GetRequiredService<IOptions<MercadoPagoConfig>>().Value;

    httpClient.DefaultRequestHeaders.Authorization = 
        new AuthenticationHeaderValue("Bearer", builder.Configuration.GetSection("ApiMercadoPago")["AccessToken"]);
    httpClient.BaseAddress = new Uri(builder.Configuration.GetSection("ApiMercadoPago")["BaseUrl"]); 
})
.AddPolicyHandler(retryPolicy);

var app = builder.Build();

app.UseDatabaseConfiguration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwaggerConfiguration();

app.UseMapEndpointsConfiguration();

app.AddGlobalErrorHandler();

app.UseStaticFiles();

app.Run();