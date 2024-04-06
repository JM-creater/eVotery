using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.Helpers.EmailContent;
using OnlineVotingSystem.Persistence.Helpers.GenerateTokens;
using OnlineVotingSystem.Persistence.Helpers.MapperProfile;
using OnlineVotingSystem.Persistence.MainFeatures.AdminFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.AdminFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.BallotFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.BallotFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.CandidateFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.DocumentsFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.DocumentsFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.ElectionFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.ElectionFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.PartyAffiliationFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.PartyAffiliationFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.PositionFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.SearchFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.SearchFeatures.Services;
using OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.IServices;
using OnlineVotingSystem.Persistence.MainFeatures.VoterFeatures.Services;

namespace OnlineVotingSystem.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("default");

        services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICandidateService, CandidateService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ISearchService, SearchService>();
        services.AddScoped<IBallotService, BallotService>();
        services.AddScoped<IElectionService, ElectionService>();
        services.AddScoped<IPartyAffiliationService, PartyAffiliationService>();
        services.AddScoped<IPersonalDocumentsService, PersonalDocumentsService>();

        services.AddScoped<Tokens>();
        services.AddScoped<EmailContentProvider>();

        services.AddAutoMapper(typeof(AutoMapperConfig));

        services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    }
}
