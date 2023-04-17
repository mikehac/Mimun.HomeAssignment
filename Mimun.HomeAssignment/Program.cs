using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Mimun.HomeAssignment.Auth;
using Mimun.HomeAssignment.Extensions;
using Mimun.HomeAssignment.Middleware;
using Mimun.HomeAssignment.ServicesConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.RegisterDb(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

builder.Services.AddControllers();
builder.Services.AddMemoryCache();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
IMapper mapper = MapperConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "Mimun WebAPI");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandler>();

app.MapControllers();
app.UseCors(x =>
    x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
);

app.Run();
