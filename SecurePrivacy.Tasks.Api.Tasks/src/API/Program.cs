using API.AutoMapper;
using API.ExtensionMethods;
using DataAccess;
using Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Secure Privacy API - V1", Version = "v1.0" });
});
builder.Services.ConfigureMongoDriver(builder.Configuration);
builder.Services.AddApiVersioningConfiguration();
builder.Services.ConfigureDataAccessDependencies();
builder.Services.ConfigureInfrastructureDependencies();
builder.Services.AddAutoMapper(typeof(ApiProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();