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

//Setting mapster
builder.Services.RegisterMaps();

var app = builder.Build();

app.UseDatabaseConfiguration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwaggerConfiguration();

app.UseMapEndpointsConfiguration();

app.AddGlobalErrorHandler();

app.Run();