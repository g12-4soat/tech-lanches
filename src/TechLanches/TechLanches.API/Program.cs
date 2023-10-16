using Microsoft.EntityFrameworkCore;
using TechLanches.API.Endpoints;
using TechLanches.API.Extensions;
using TechLanches.Application;
using TechLanches.Domain.Repositories;
using TechLanches.Domain.Services;
using TechLanches.Infrastructure;
using TechLanches.Infrastructure.Repositories;
using static TechLanches.Infrastructure.TechLanchesDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContexts(builder.Configuration);
builder.Services.AddIntegrationServices();
builder.Services.AddIntegrationRepository();
builder.Services.RegisterMaps();


var app = builder.Build();

// mover para extensão
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<TechLanchesDbContext>();

    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }

    DataSeeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// mover para extensão
app.MapClienteEndpoints();
app.MapProdutoEndpoints();

app.Run();