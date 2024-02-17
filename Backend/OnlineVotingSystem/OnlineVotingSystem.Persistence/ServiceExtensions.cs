using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.Helpers.MapperProfile;
using OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.Services;

namespace OnlineVotingSystem.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("default");

        services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));

        services.AddScoped<IVoterService, VoterService>();
        services.AddScoped<ICandidateService, CandidateService>();
        services.AddScoped<IPositionService, PositionService>();

        services.AddAutoMapper(typeof(AutoMapperConfig));

        services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    }
}
