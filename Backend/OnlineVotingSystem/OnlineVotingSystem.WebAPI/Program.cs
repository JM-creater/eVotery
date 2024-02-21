using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Application;
using OnlineVotingSystem.Persistence;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.Configure<ImagePathOptions>(builder.Configuration.GetSection("ImagePath"));

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
await dataContext?.Database.MigrateAsync();

app.ConfigureApplication(builder.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandler();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
