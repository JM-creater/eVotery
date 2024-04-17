using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OnlineVotingSystem.Application;
using OnlineVotingSystem.Application.ImageDirectory;
using OnlineVotingSystem.Persistence;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.WebAPI.Extensions;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("JWT");

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("X-API-KEY", new OpenApiSecurityScheme
    {
        Name = "X-API-KEY",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme",
        In = ParameterLocation.Header,
        Description = "ApiKey must appear in header"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "X-API-KEY"
                },
                In = ParameterLocation.Header
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddHealthChecks();

builder.Services.Configure<ImagePathOptions>(builder.Configuration.GetSection("ImagePath"));

builder.Services.AddHttpClient();

builder.Services.AddRateLimiter(_ => _
       .AddFixedWindowLimiter(policyName: "fixed", options =>
       {
           options.PermitLimit = 4;
           options.Window = TimeSpan.FromSeconds(12);
           options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
           options.QueueLimit = 2;
       }));

var app = builder.Build();

app.MapHealthChecks("/health");

var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
await dataContext?.Database.MigrateAsync();

app.ConfigureApplication(builder.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseErrorHandler();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
