using Microsoft.EntityFrameworkCore;
using TechLanches.API.Configuration;
using TechLanches.API.Endpoints;
using TechLanches.Application;
using TechLanches.Domain.Repositories;
using TechLanches.Domain.Services;
using TechLanches.Infrastructure;
using TechLanches.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Setting Swagger
builder.Services.AddSwaggerConfiguration();

//DI Abstraction
builder.Services.AddDependencyInjectionConfiguration();

//Setting DBContext
builder.Services.AddDatabaseConfiguration(builder.Configuration);

var app = builder.Build();

app.UseDatabaseConfiguration();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    var context = services.GetRequiredService<TechLanchesDbContext>();

//    if (context.Database.GetPendingMigrations().Any())
//    {
//        context.Database.Migrate();
//    }
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwaggerConfiguration();

app.UseMapEndpointsConfiguration();

app.Run();