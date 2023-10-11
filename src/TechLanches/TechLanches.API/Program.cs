using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSwaggerGen();

// mover para extensão
builder.Services.AddDbContext<TechLanchesDbContext>(config => 
    config.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// mover para extensão
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IProdutoService, ProdutoService>();


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
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// mover para extensão
app.MapClienteEndpoints();

app.Run();