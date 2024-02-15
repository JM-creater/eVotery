using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Application;
using OnlineVotingSystem.Application.ImageDirectory;
using OnlineVotingSystem.Persistence;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.WebAPI.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ImagePathOptions>(builder.Configuration.GetSection("ImagePath"));

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
await dataContext?.Database.MigrateAsync();

//string logpath = builder.Configuration.GetSection("Logging:Logpath").Value;

//var _logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .MinimumLevel.Override("microsoft", Serilog.Events.LogEventLevel.Warning)
//    .Enrich.FromLogContext()
//    .WriteTo.File(logpath)
//    .CreateLogger();

//builder.Logging.AddSerilog(_logger);

app.ConfigureApplication(builder.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandler();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
